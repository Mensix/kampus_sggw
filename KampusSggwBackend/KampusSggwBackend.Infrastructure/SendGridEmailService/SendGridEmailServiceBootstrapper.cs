namespace KampusSggwBackend.Infrastructure.SendGridEmailService;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class SendGridEmailServiceBootstrapper
{
    public static IServiceCollection AddSendGridEmailService(this IServiceCollection services, IConfiguration configuration)
    {
        var sendGridSettings = new SendGridSettings();
        configuration.Bind("SendGrid", sendGridSettings);
        services.AddSingleton(sendGridSettings);

        services.AddTransient<IEmailSender, SendGridEmailSender>();
        services.AddTransient<IEmailService, SendGridEmailService>();

        return services;
    }
}
