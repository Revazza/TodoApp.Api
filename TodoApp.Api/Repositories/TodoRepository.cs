using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Db;
using TodoApp.Api.Db.Entities;
using TodoApp.Api.Models.Requests;

namespace TodoApp.Api.Repositories
{
    public interface ITodoRepository
    {
        Task<TodoEntity> CreateTodoAsync(Guid userId, CreateTodoRequest request);
        Task SaveChangesAsync();
        Task<List<TodoEntity>> GetAllTodo(Guid userId);
        Task<List<TodoEntity>> SearchTodo(Guid userId, SearchTodoRequest request);
    }

    public class TodoRepository : ITodoRepository
    {
        private readonly TodoAppDbContext _context;

        public TodoRepository(TodoAppDbContext context)
        {
            _context = context;
        }

        public async Task<TodoEntity> CreateTodoAsync(Guid userId, CreateTodoRequest request)
        {

            var todo = new TodoEntity()
            {
                Name = request.Name,
                Deadline = request.Deadline,
                Description = request.Description,
                CreatorId = userId,
            };

            await _context.Todos.AddAsync(todo);

            return todo;
        }

        public async Task<List<TodoEntity>> GetAllTodo(Guid userId)
        {
            return await _context.Todos
                .Where(t => t.CreatorId == userId)
                .ToListAsync();
        }
        public Task<List<TodoEntity>> SearchTodo(Guid userId, SearchTodoRequest request)
        {


            return null;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}
