using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Models.Requests;

namespace TodoApp.Api.Controllers
{
    [Route("Todo")]
    [ApiController]
    public class TodoController : ControllerBase
    {


        [HttpPost("add-todo")]
        public async Task<IActionResult> CreateTodo(CreateTodoRequest request)
        {


            return Ok();
        }


    }
}
