using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using TodoApp.Api.Db;
using TodoApp.Api.Db.Entities;
using TodoApp.Api.Models.Dtos;
using TodoApp.Api.Models.Requests;

namespace TodoApp.Api.Repositories
{
    public interface ITodoRepository
    {
        Task<TodoEntity> CreateTodoAsync(Guid userId, CreateTodoRequest request);
        Task SaveChangesAsync();
        Task<List<TodoEntity>> GetAllTodoAsync(Guid userId);
        Task<List<TodoEntity>> SearchTodoAsync(Guid userId, SearchTodoDto queries);
        Task DeleteTodoAsync(Guid todoId);
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

        public async Task<List<TodoEntity>> GetAllTodoAsync(Guid userId)
        {
            return await _context.Todos
                .Where(t => t.CreatorId == userId)
                .ToListAsync();
        }
        public async Task<List<TodoEntity>> SearchTodoAsync(Guid userId, SearchTodoDto queries)
        {
            var todos = _context.Todos as IQueryable<TodoEntity>;

            todos = todos.Where(t => t.CreatorId == userId);

            if (!string.IsNullOrEmpty(queries.Name))
            {
                todos = todos.Where(t => t.Name!.ToLower().Contains(queries.Name.ToLower()));
            }
            if (!string.IsNullOrEmpty(queries.Description))
            {
                todos = todos.Where(t => t.Description!.ToLower().Contains(queries.Description.ToLower()));
            }
            if (queries.Status != Status.None)
            {
                todos = todos.Where(t => t.Status == queries.Status);
            }

            return await todos.ToListAsync();
        }
        public async Task DeleteTodoAsync(Guid todoId)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(t => t.Id == todoId);

            if (todo != null)
            {
                _context.Remove(todo);
            }

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
