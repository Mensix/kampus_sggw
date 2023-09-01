namespace KampusSggwBackend.Configuration;

using KampusSggwBackend.Data;
using Microsoft.EntityFrameworkCore;

internal static class DatabaseInitializer
{
    internal static void InitializeDatabase(this IApplicationBuilder app)
    {
        var serviceFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

        using var serviceScope = serviceFactory.CreateScope();
        using var dbContext = serviceScope.ServiceProvider.GetService<DataContext>();

        dbContext.Database.Migrate();
    }
}
