namespace TodoApp.Api.Db.RequestEntities
{
    public class SendResetPasswordRequestEntity : BaseRequestEntity
    {
        public string? UserId { get; set; }
        public string? NewPassword { get; set; }

    }
}
