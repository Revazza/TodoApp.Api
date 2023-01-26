using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Models.Requests;
using TodoApp.Api.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using TodoApp.Api.Auth;

namespace TodoApp.Api.Controllers
{
    [Route("Authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly TokenGenerator _tokenGenerator;

        public AuthenticationController(
            IAuthService authService,
            TokenGenerator tokenGenerator
            )
        {
            _authService = authService;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            //TODO:
            //      Notify user to activate account by clicking link at gmail
            try
            {
                var claims = await _authService.LoginAsync(request);
                var token = _tokenGenerator.GenerateToken(claims.ToList());

                return Ok(token);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterUserRequest request)
        {
            try
            {
                await _authService.RegisterAsync(request);
                await _authService.SaveChangesAsync();

                return Ok($"Confirmation link was sent to your gmail {request.Email}");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            try
            {
                await _authService.ConfirmEmailAsync(userId, token);
                await _authService.SaveChangesAsync();
                return Ok("Email Confirmed!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordRequest(ResetPasswordRequest request)
        {
            try
            {
                await _authService.ResetPasswordRequestAsync(request);
                await _authService.SaveChangesAsync();
                return Ok("Check mail");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet("reset-password")]
        public async Task<IActionResult> ResetPassword(string userId, string token)
        {
            try
            {
                await _authService.ResetPasswordAsync(userId, token);
                await _authService.SaveChangesAsync();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

            return Ok("Everything's cool");
        }

    }
}
