namespace KampusSggwBackend.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static void AddRelationalDb(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DataContext>(o =>
        {
            o.UseSqlServer(connectionString);
            o.EnableSensitiveDataLogging();
        });
    }
}