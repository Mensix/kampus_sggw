namespace KampusSggwBackend.Configuration;

using KampusSggwBackend.Data;
using KampusSggwBackend.Domain.User;
using KampusSggwBackend.Services.JwtToken;
using KampusSggwBackend.Services.JwtToken.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

public static class AuthConfig
{
    public static void AddAppAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<UserAccount, UserRole>()
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();

        services.AddIdentityCore<UserAccount>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequiredUniqueChars = 1;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
            options.Lockout.MaxFailedAccessAttempts = 10;
            options.SignIn.RequireConfirmedEmail = true;
            options.SignIn.RequireConfirmedPhoneNumber = false;
            options.ClaimsIdentity.UserIdClaimType = JwtRegisteredClaimNames.NameId;
            options.User.RequireUniqueEmail = true;
        });

        var jwtSettings = new JwtSettings();
        configuration.Bind("Jwt", jwtSettings);

        services.AddSingleton<IJwtTokenService>(serviceProvider => new JwtTokenService(jwtSettings));
        services.AddTransient<IRevokedTokensRepository, RevokedTokensRepository>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtSettings.Issuer,

                ValidateAudience = true,
                ValidAudience = jwtSettings.Audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = jwtSettings.GetSigningKey(),

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };
        });
    }
}
