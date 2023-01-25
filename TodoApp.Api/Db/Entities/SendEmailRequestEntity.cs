namespace TodoApp.Api.Db.Entities
{
    public class SendEmailRequestEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Title { get; set; }
        public string? Body { get; set; }
        // user email address
        public string? ToAddress { get; set; }
        public string? ConfirmationToken { get; set; }



    }
}
