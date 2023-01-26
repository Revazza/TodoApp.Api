namespace TodoApp.Api.Db.RequestEntities
{
    public enum RequestStatus
    {
        NotSent,
        Failed,
        Sent,
    }
    public class BaseRequestEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Subject { get; set; }
        public string? Body { get; set; }
        // user email address
        public string? ToAddress { get; set; }
        public RequestStatus Status { get; set; } = RequestStatus.NotSent;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public override string? ToString()
        {
            return $"User Email:{ToAddress}\n" +
                    $"Subject:{Subject}\n" +
                    $"Body:{Body}\n" +
                    $"Status:{Status}\n" + "\n";
        }
    }
}
