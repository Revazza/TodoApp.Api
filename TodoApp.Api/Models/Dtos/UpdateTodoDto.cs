using TodoApp.Api.Db.Entities;

namespace TodoApp.Api.Models.Dtos
{
    public class UpdateTodoDto
    {
        public Guid TodoId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }
        public DateTime Deadline { get; set; }
    }
}
