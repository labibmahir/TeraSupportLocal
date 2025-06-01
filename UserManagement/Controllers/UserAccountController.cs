using Domain.Dtos;
using Domain.Entities;
using Infrastructure;
using JWTConfiguration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utilities.Constants;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IUnitOfWork context;
        private readonly JwtExtensions _jwtExtensions;

        public UserAccountController(IUnitOfWork context, JwtExtensions jwtExtensions)
        {
            this.context = context;
            _jwtExtensions = jwtExtensions;
        }

        [HttpPost]
        public async Task<ActionResult<UserAccount>> CreateUserAccount(UserAccount userAccount)
        {
            try
            {
                userAccount.Oid = Guid.NewGuid();
                userAccount.DateCreated = DateTime.Now;
                userAccount.OrganizationId = Guid.NewGuid();
                userAccount.IsDeleted = false;

                EncryptionHelpers encryptionHelpers = new EncryptionHelpers();
                string encryptedPassword = encryptionHelpers.Encrypt(userAccount.Password);
                userAccount.Password = encryptedPassword;

                context.UserAccountRepository.Add(userAccount);
                await context.SaveChangesAsync();

                return Ok(userAccount);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<UserAccount>> GetUserAccount(Guid organisationId)
        {
            try
            {
                var users = await context.UserAccountRepository.GetUserAccounts(organisationId);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequest request)
        {
            EncryptionHelpers encryptionHelpers = new EncryptionHelpers();
            string encryptedPassword = encryptionHelpers.Encrypt(request.Password);
            request.Password = encryptedPassword;

            if (request == null || string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(encryptedPassword))
                return BadRequest(new { message = "Email and password are required." });

            var authResponse = await _jwtExtensions.GenerateJwtToken(request);

            if (authResponse == null)
                return Unauthorized(new { message = "Invalid email or password." });

            return Ok(authResponse);
        }

        [HttpGet("user/{userId}")]
        public async Task<ApiResponse<UserAccount>> GetUserById(Guid userId)
        {
            try
            {
                var user = await context.UserAccountRepository.GetUserAccountByKey(userId);
                if (user == null)
                {
                    return ApiResponse<UserAccount>.Fail(MessageConstants.NotFound);
                }

                return ApiResponse<UserAccount>.Success(user);
            }
            catch (Exception ex)
            {
                return ApiResponse<UserAccount>.Fail(MessageConstants.GenericError);
            }
        }
    }
}
