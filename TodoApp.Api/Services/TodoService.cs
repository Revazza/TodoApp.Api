using Microsoft.AspNetCore.Identity;
using TodoApp.Api.Db.Entities;
using TodoApp.Api.Models.Requests;

namespace TodoApp.Api.Services
{
    public interface ITodoService
    {
        Task<TodoEntity> CreateTodo(CreateTodoRequest request);

    }

    public class TodoService : ITodoService
    {
        private readonly UserManager<UserEntity> _userManager;

        public TodoService(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public Task<TodoEntity> CreateTodo(CreateTodoRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
