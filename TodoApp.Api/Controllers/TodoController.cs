using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoApp.Api.Models.Requests;
using Microsoft.AspNetCore.Identity;
using TodoApp.Api.Db.Entities;
using Microsoft.AspNetCore.Authorization;

namespace TodoApp.Api.Controllers
{
    [Route("Todo")]
    [ApiController]
    public class TodoController : ControllerBase
    {

        [Authorize("ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpPost("add-todo")]
        public async Task<IActionResult> CreateTodo(CreateTodoRequest request)
        {
            var claimsPrincipal = User;

            

            return Ok();
        }


    }
}
