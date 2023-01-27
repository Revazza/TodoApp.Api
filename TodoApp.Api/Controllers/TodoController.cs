using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TodoApp.Api.Db.Entities;
using TodoApp.Api.Repositories;
using TodoApp.Api.Models.Dtos;

namespace TodoApp.Api.Controllers
{
    [Authorize("ApiUser", AuthenticationSchemes = "Bearer")]
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

        [HttpGet("{userId}")]
        public async Task<IEnumerable<TodoEntity>> GetAllUserTodo(Guid userId)
        {
            return await _todoRepository.GetAllTodoAsync(userId);
        }

        [HttpGet("search-todo")]
        public async Task<IEnumerable<TodoEntity>> SearchTodo(
            Guid userId,
            string? name,
            string? description,
            Status status
            )
        {
            var queries = new SearchTodoDto()
            {
                Status = status,
                Description = description,
                Name = name,
            };



            return await _todoRepository.SearchTodoAsync(userId, queries); ;
        }


        [HttpDelete("{todoId}")]
        public async Task<IActionResult> DeleteTodo(Guid todoId)
        {

            await _todoRepository.DeleteTodoAsync(todoId);
            await _todoRepository.SaveChangesAsync();
            return Ok("Todo deleted");
        }

        [HttpPost("update-todo")]
        public async Task<IActionResult> UpdateTodo(UpdateTodoDto parameters)
        {
            try
            {
                var updatedTodo = await _todoRepository.UpdateTodoAsync(parameters);
                await _todoRepository.SaveChangesAsync();

                return Ok(updatedTodo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }

    }
}
