using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace JWTConfiguration;

public class JwtExtensions
{
    public const string JWT_SECURITY_KEY =
        "81a02c70b55a9686258843cc539d1e8c9de4fb701806c53900876758d968e0290d7fd85b7389289aaf52d3481c4c918ec95fcecf2020d899230d268f0bc55eb203316e9606a5c8947c7624abd33c33c56e1ac09b7d5ac399d193e31a05fd517eb04468058c1c8b0816b91883dbf17fe05c28950c135529e69a8bd9d578d4dc1a38cd99867c5ab9b5c19eb086dff1c371f2b8584d5756c7fa178cb0a659ec5684df5f0db578074fb1de69680e2a8da04c26074e251de8a965701d24fd03cbd77299bbafeb07f56c1aa8498788877090f79d74ab11fadfd956b71f06ca73565e8f6f51faa57151037c05c096521a5acc5df79f8d29caa09ea378e7617b6c43a6095c6aad561d41f0217605302d526d31db900257e2c0e24ff4d1f56781f2816d994260a99bc29d8590e3381a37bd5a18122c9b86b1ca37eb8dee734ffca4c2434403c60f10c55e9805e49f1a5218a1fa716cec07c9910df075bbf77cba62ae9d322da4820e65795d38b75b8bbcfe4115a63a26992868b62b0ae8b83daed96abfa6661be944e91b4612fd70b3e8446427c2f72feb650e1376317799962f14fb2d64ef42066d6f22df447b85feff15429ca34882b4824288dce72c455a9014926851bb5aa3b0ddfd9da9002a7be0a2bb19bdefd281cc286305663aacabdc9c6ce0432d93c9adb40e204ab81628e1105ec979b2968d1785d9d99294914512dc951aee";

    private const int JWT_TOKEN_VALIDITY_MINS = 20;
    private readonly DataContext dataContext;

    public JwtExtensions(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public async Task<AuthenticationResponse>? GenerateJwtToken(AuthenticationRequest authenticationRequest)
    {
        if (string.IsNullOrWhiteSpace(authenticationRequest.Email) ||
            string.IsNullOrWhiteSpace(authenticationRequest.Password))
            return null;

        var userAccount = await dataContext.UserAccounts
            .Where(x =>
                x.Email == authenticationRequest.Email &&
                x.Password == authenticationRequest.Password &&
                x.IsDeleted == false)
            .FirstOrDefaultAsync();

        if (userAccount == null)
            return null;

        var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
        var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Name, authenticationRequest.Email)
        };

        if (userAccount.OrganizationId != null && userAccount.OrganizationId != Guid.Empty)
        {
            claims.Add(new Claim("OrganisationId", userAccount.OrganizationId.ToString()));
        }

        var claimsIdentity = new ClaimsIdentity(claims);

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(tokenKey),
            SecurityAlgorithms.HmacSha256Signature);

        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Expires = tokenExpiryTimeStamp,
            SigningCredentials = signingCredentials
        };

        var jwtSecurityHandler = new JwtSecurityTokenHandler();
        var securityToken = jwtSecurityHandler.CreateToken(securityTokenDescriptor);
        var token = jwtSecurityHandler.WriteToken(securityToken);

        // Generate refresh token as JWT (1 day expiry)
        var refreshToken = GenerateRefreshToken(authenticationRequest.Email, userAccount.OrganizationId ?? Guid.Empty);

        return new AuthenticationResponse
        {
            Email = authenticationRequest.Email,
            OrganisationId = userAccount.OrganizationId ?? Guid.Empty,
            ExpireIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
            JwtToken = token,
            RefreshToken = refreshToken // Add RefreshToken property to AuthenticationResponse below
        };
    }
    
    public AuthenticationResponse? RefreshJwtToken(string refreshToken)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JWT_SECURITY_KEY)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true // refresh token must be valid (not expired)
        };

        var jwtSecurityHandler = new JwtSecurityTokenHandler();
        try
        {
            var principal =
                jwtSecurityHandler.ValidateToken(refreshToken, tokenValidationParameters, out var validatedToken);

            if (validatedToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
                return null;

            var email = principal.Identity?.Name;
            var organisationIdClaim = principal.FindFirst("OrganisationId")?.Value;

            if (string.IsNullOrEmpty(email))
                return null;

            var organisationId = Guid.Empty;
            if (!string.IsNullOrEmpty(organisationIdClaim))
                Guid.TryParse(organisationIdClaim, out organisationId);

            // Generate new access token with short expiry
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, email)
            };
            if (organisationId != Guid.Empty)
            {
                claims.Add(new Claim("OrganisationId", organisationId.ToString()));
            }

            var claimsIdentity = new ClaimsIdentity(claims);

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            var newJwtToken = jwtSecurityHandler.CreateToken(securityTokenDescriptor);
            var newAccessToken = jwtSecurityHandler.WriteToken(newJwtToken);

            // Generate new refresh token
            var newRefreshToken = GenerateRefreshToken(email, organisationId);

            return new AuthenticationResponse
            {
                Email = email,
                OrganisationId = organisationId,
                ExpireIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                JwtToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
        catch
        {
            // Invalid token
            return null;
        }
    }

    private string GenerateRefreshToken(string email, Guid organisationId)
    {
        var tokenExpiryTimeStamp = DateTime.Now.AddDays(1); // 1 day expiry
        var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Email, email)
        };

        if (organisationId != Guid.Empty)
        {
            claims.Add(new Claim("OrganisationId", organisationId.ToString()));
        }

        var claimsIdentity = new ClaimsIdentity(claims);

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(tokenKey),
            SecurityAlgorithms.HmacSha256Signature);

        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Expires = tokenExpiryTimeStamp,
            SigningCredentials = signingCredentials
        };

        var jwtSecurityHandler = new JwtSecurityTokenHandler();
        var securityToken = jwtSecurityHandler.CreateToken(securityTokenDescriptor);
        return jwtSecurityHandler.WriteToken(securityToken);
    }
}