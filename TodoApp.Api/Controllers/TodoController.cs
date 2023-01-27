using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TodoApp.Api.Db.Entities;
using TodoApp.Api.Repositories;

namespace TodoApp.Api.Controllers
{
    [Route("Todo")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ITodoRepository _todoRepository;

        public TodoController(
            UserManager<UserEntity> userManager,
            ITodoRepository todoRepository)
        {
            _userManager = userManager;
            _todoRepository = todoRepository;
        }



        [Authorize("ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpPost("add-todo")]
        public async Task<IActionResult> CreateTodo(CreateTodoRequest request)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return BadRequest("Can't identify user");
            }

            var newTodo = await _todoRepository.CreateTodoAsync(user.Id, request);

            await _todoRepository.SaveChangesAsync();


            return Created($"Todos/{newTodo.Id}", newTodo);
        }


    }
}
