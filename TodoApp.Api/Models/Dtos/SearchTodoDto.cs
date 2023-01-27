using TodoApp.Api.Db.Entities;

namespace TodoApp.Api.Models.Dtos
{
    public class SearchTodoDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; } = Status.InProcess;


    }
}
