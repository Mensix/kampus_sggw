namespace KampusSggwBackend.Infrastructure.SendGridEmailService;

using System.Threading.Tasks;

public interface IEmailService
{
    public Task SendEmailConfirmationForNewAccount(string email, string verificationCode);
    public Task SendNewEmailConfirmationMessage(string email, string confirmationLink);
    public Task SendPasswordResetCodeMessage(string email, string code);
    public Task SendNewVerificationCodeMessage(string email, string verificationCode);
    public Task SendWelcomeToAnnMessage(string email);
}
