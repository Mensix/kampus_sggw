namespace KampusSggwBackend.Infrastructure.SendGridEmailService;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string message);
    Task SendEmailAsync(string email, string templateId, object templateData);
}
