namespace KampusSggwBackend.Infrastructure.ScheduleExcelFileGenerator;

using Microsoft.Extensions.DependencyInjection;

public static class ScheduleExcelFileGeneratorBootstrapper
{
    public static IServiceCollection AddScheduleExcelFileGenerator(this IServiceCollection services)
    {
        // Repositories
        // services.AddTransient<ILecturersRepository, LecturersRepository>();

        return services;
    }
}
