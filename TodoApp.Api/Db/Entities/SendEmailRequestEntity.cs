namespace TodoApp.Api.Db.Entities
{
    public enum EmailStatus
    {
        NotSent,
        Failed,
    }
    public class SendEmailRequestEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? UserId { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        // user email address
        public string? ToAddress { get; set; }
        public string? ConfirmationToken { get; set; }
        public EmailStatus Status { get; set; } = EmailStatus.NotSent;

        public override string? ToString()
        {
            return $"User Email:{ToAddress}\n" +
                    $"Subject:{Subject}\n" +
                    $"Body:{Body}\n" +
                    $"Status:{Status}\n";
        }
    }
}
