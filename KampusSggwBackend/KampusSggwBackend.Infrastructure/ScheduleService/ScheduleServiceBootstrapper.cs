namespace KampusSggwBackend.Infrastructure.ScheduleService;

using KampusSggwBackend.Infrastructure.ScheduleService.Repositories.Classes;
using KampusSggwBackend.Infrastructure.ScheduleService.Repositories.Lecturers;
using KampusSggwBackend.Infrastructure.ScheduleService.Repositories.Lessons;
using Microsoft.Extensions.DependencyInjection;

public static class ScheduleServiceBootstrapper
{
    public static IServiceCollection AddScheduleService(this IServiceCollection services)
    {
        // Repositories
        services.AddTransient<ILecturersRepository, LecturersRepository>();
        services.AddTransient<IClassesRepository, ClassesRepository>();
        services.AddTransient<ILessonsRepository, LessonsRepository>();

        return services;
    }
}
