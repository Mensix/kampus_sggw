namespace KampusSggwBackend.Infrastructure.UserService;

using KampusSggwBackend.Infrastructure.UserService.Repositories.PasswordResetRequests;
using KampusSggwBackend.Infrastructure.UserService.Repositories.UserEmailChanges;
using KampusSggwBackend.Infrastructure.UserService.Repositories.Users;
using KampusSggwBackend.Infrastructure.UserService.Repositories.VerificationCodes;
using Microsoft.Extensions.DependencyInjection;

public static class UserServiceBootstrapper
{
    public static IServiceCollection AddUsersRepository(this IServiceCollection services)
    {
        // Repositories
        services.AddTransient<IUsersRepository, UsersRepository>();
        services.AddTransient<IVerificationCodesRepository, VerificationCodesRepository>();
        services.AddTransient<IPasswordResetRequestsRepository, PasswordResetRequestsRepository>();
        services.AddTransient<IUserEmailChangesRepository, UserEmailChangesRepository>();
        
        // Services
        services.AddTransient<IVerificationCodeFactoryService, VerificationCodeFactoryService>();

        return services;
    }
}
