using TodoApp.Api.Db.Entities;

namespace TodoApp.Api.Models.Requests
{
    public class SearchTodoRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; } = Status.InProcess;
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }


    }
}
