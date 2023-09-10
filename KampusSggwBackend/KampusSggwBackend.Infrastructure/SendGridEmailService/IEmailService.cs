namespace KampusSggwBackend.Infrastructure.SendGridEmailService;

using System.Threading.Tasks;

public interface IEmailService
{
    Task SendEmailConfirmationForNewAccount(string email, string verificationCode);
    Task SendNewEmailConfirmationMessage(string email, string confirmationLink);
    Task SendPasswordResetCodeMessage(string email, string code);
    Task SendNewVerificationCodeMessage(string email, string verificationCode);
    Task SendWelcomeToAnnMessage(string email);
    Task SendEmailAfterAccountDelete(string email);
}
