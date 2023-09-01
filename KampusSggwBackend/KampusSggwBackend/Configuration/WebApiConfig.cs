namespace KampusSggwBackend.Configuration;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Text.Json.Serialization;
using System.Text.Json;
using KampusSggwBackend.Configuration.Middlewares;
using KampusSggwBackend.Services.RequestingUser;

internal static class WebApiConfig
{
    internal static void AddAnnWebApi(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<IRequestingUserService, RequestingUserService>();

        services.AddControllers(options =>
        {
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            options.Filters.Add(new AuthorizeFilter(policy));
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.AddRazorPages();
    }

    internal static void UseAnnWebApi(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapRazorPages();
        });
    }
}
