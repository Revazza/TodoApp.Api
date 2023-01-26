namespace TodoApp.Api.Models.Requests
{
    public class ResetPasswordRequest
    {
        public string? Email { get; set; }
        public string? NewPassword { get; set; }

    }
}
