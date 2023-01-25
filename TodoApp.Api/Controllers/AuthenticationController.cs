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


        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserRequest request)
        {

            _authService.LoginAsync(new LoginRequest());

            return Ok();
        }
    }
}
