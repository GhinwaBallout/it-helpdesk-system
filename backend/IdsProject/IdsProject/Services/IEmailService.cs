namespace IdsProject.Services
{
    public interface IEmailService
    {
        Task SendPasswordResetEmailAsync(string email, string token);
    }
}