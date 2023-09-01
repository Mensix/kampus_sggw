namespace KampusSggwBackend.Configuration;

using Microsoft.OpenApi.Models;
using System.Reflection;

public static class SwaggerConfig
{
    public static IServiceCollection AddAppSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            const string version = "v1";

            options.SwaggerDoc(version, new OpenApiInfo
            {
                Title = "Kampus Sggw Backend API",
                Version = version
            });

            var assemblyName = Assembly.GetEntryAssembly()?.GetName().Name;
            var xmlCommentsFile = $"{assemblyName}.xml";
            var xmlCommentsFilePath = Path.Combine(System.AppContext.BaseDirectory, xmlCommentsFile);
            options.IncludeXmlComments(xmlCommentsFilePath);

            options.CustomSchemaIds(type => type.FullName);

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        return services;
    }

    public static IApplicationBuilder UseAppSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}
