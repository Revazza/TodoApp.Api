using System.ComponentModel.DataAnnotations;

namespace TodoApp.Api.Db.Entities
{
    public enum Status
    {
        InProcess = 0,
        Completed,
        None

    }

    public class TodoEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; } = Status.InProcess;
        public DateTime Deadline{ get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid CreatorId { get; set; }




    }
}
