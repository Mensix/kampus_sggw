namespace KampusSggwBackend.Infrastructure.SendGridEmailService;

using System.Threading.Tasks;

public class SendGridEmailService : IEmailService
{
    // Services
    private readonly IEmailSender _emailSender;

    // Constructor
    public SendGridEmailService(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    // Methods
    public Task SendEmailConfirmationForNewAccount(string email, string verificationCode)
    {
        var subject = "Kod weryfikacyjny - Kampus SGGW";
        var message = $"W celu weryfikacji maila użytkownika proszę wpisać " +
            $"następujący kod w aplikacji Kampus SGGW: {verificationCode}. " +
            $"Kod jest ważny przez 4 godziny.";


        return _emailSender.SendEmailAsync(email, subject, message);
    }

    public Task SendNewEmailConfirmationMessage(string email, string confirmationLink)
    {
        var subject = "Kod weryfikacyjny - Kampus SGGW";
        var message = $"W celu potwierdzenia maila użytkownika użyj " +
            $"następującego linku w aplikacji Kampus SGGW: {confirmationLink}.";

        return _emailSender.SendEmailAsync(email, subject, message);
    }

    public Task SendPasswordResetCodeMessage(string email, string code)
    {
        var subject = "Kod weryfikacyjny - Kampus SGGW";
        var message = $"W celu zresetowania hasła proszę wpisać " +
            $"następujący kod w aplikacji Kampus SGGW: {code}.";

        return _emailSender.SendEmailAsync(email, subject, message);
    }

    public Task SendNewVerificationCodeMessage(string email, string verificationCode)
    {
        var subject = "Nowy kod weryfikacyjny - Kampus SGGW";
        var message = $"W celu weryfikacji maila użytkownika proszę wpisać " +
            $"następujący kod w aplikacji Kampus SGGW: {verificationCode}. " +
            $"Kod jest ważny przez 4 godziny.";

        return _emailSender.SendEmailAsync(email, subject, message);
    }

    public Task SendWelcomeToAnnMessage(string email)
    {
        var subject = "Wityamy w aplikacji Kampus SGGW";
        var message = $"Wityamy w aplikacji Kampus SGGW";

        return _emailSender.SendEmailAsync(email, subject, message);
    }

    public Task SendEmailAfterAccountDelete(string email)
    {
        var subject = "Usunięcię konta w aplikacji Kampus SGGW";
        var message = $"Twoje konto zostało pomyślnie usunięte z aplikacji Kampus SGGW";

        return _emailSender.SendEmailAsync(email, subject, message);
    }
}
