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

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {


            return Ok();
        }


    }
}
