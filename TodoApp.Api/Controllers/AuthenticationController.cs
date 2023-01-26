using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Models.Requests;
using TodoApp.Api.Services;

namespace TodoApp.Api.Controllers
{
    [Route("Authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(
            IAuthService authService
            )
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            //TODO:
            //      Notify user to activate account by clicking link at gmail
            try
            {

                var token = await _authService.LoginAsync(request);

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
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            try
            {
                await _authService.ResetPasswordAsync(request);
                await _authService.SaveChangesAsync();
                return Ok("Check mail");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet("reset-password")]
        public async Task<IActionResult> ResetPassword(string userId,string token)
        {


            return Ok("Everything's cool");
        }

    }
}
