namespace KampusSggwBackend.Infrastructure.UserService;

using KampusSggwBackend.Infrastructure.UserService.Repositories.Users;
using Microsoft.Extensions.DependencyInjection;

public static class UserServiceBootstrapper
{
    public static IServiceCollection AddUsersRepository(this IServiceCollection services)
    {
        // Repositories
        services.AddTransient<IUsersRepository, UsersRepository>();

        // Services
        // services.AddTransient<IVerificationCodeFactoryService, VerificationCodeFactoryService>();

        return services;
    }
}
