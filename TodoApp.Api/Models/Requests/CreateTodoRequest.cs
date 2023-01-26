using TodoApp.Api.Db.Entities;

namespace TodoApp.Api.Models.Requests
{
    public class CreateTodoRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime Deadline { get; set; }

    }
}
