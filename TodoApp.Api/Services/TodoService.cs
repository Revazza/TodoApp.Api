using TodoApp.Api.Db.Entities;
using TodoApp.Api.Models.Requests;

namespace TodoApp.Api.Services
{
    public interface ITodoService
    {
        Task<TodoEntity> CreateTodo(CreateTodoRequest request);

    }

    public class TodoService
    {
    }
}
