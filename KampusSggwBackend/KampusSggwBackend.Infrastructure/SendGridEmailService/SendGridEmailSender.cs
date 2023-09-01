namespace KampusSggwBackend.Infrastructure.SendGridEmailService;

using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

public class SendGridEmailSender : IEmailSender
{
    // Properties
    private readonly SendGridSettings _sendGridSettings;

    // Constructor
    public SendGridEmailSender(SendGridSettings sendGridSettings)
    {
        _sendGridSettings = sendGridSettings;
    }

    // Methods
    public Task SendEmailAsync(string email, string subject, string message)
    {
        return Execute(_sendGridSettings.Key, subject, message, email);
    }

    public async Task SendEmailAsync(string email, string templateId, object templateData)
    {
        var client = new SendGridClient(_sendGridSettings.Key);

        var msg = new SendGridMessage
        {
            From = new EmailAddress(_sendGridSettings.SenderEmail, _sendGridSettings.SenderName),
            TemplateId = templateId
        };

        msg.AddTo(new EmailAddress(email));
        msg.SetClickTracking(false, false);
        msg.SetTemplateData(templateData);

        var result = await client.SendEmailAsync(msg);
        await FailIfError(email, result);
    }

    private async Task Execute(string apiKey, string subject, string message, string email)
    {
        var client = new SendGridClient(apiKey);

        var msg = new SendGridMessage
        {
            From = new EmailAddress(_sendGridSettings.SenderEmail, _sendGridSettings.SenderName),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };

        msg.AddTo(new EmailAddress(email));

        msg.SetClickTracking(false, false);

        var result = await client.SendEmailAsync(msg);

        await FailIfError(email, result);
    }

    private static async Task FailIfError(string email, Response result)
    {
        if ((int)result.StatusCode >= 300)
        {
            var resultBody = await result.Body.ReadAsStringAsync();
            throw new Exception($"Email not sent, status: {result.StatusCode}, destinationEmail: {email}, resultBody: {resultBody}");
        }
    }
}
