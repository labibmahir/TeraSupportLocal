namespace JWTConfiguration;

public class AuthenticationResponse
{
    public string Email { get; set; }
    
    public Guid OrganisationId { get; set; }

    public string RefreshToken { get; set; }
    
    public string JwtToken { get; set; }

    public int ExpireIn { get; set; }
}