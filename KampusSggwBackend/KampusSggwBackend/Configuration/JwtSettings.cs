namespace KampusSggwBackend.Configuration;

using Microsoft.IdentityModel.Tokens;
using System.Text;

public class JwtSettings
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int AccessTokenValidFor { get; set; }
    public int RefreshTokenValidFor { get; set; }
    public string SigningKey { get; set; }

    public static DateTime NotBefore() => DateTime.UtcNow;
    public static DateTimeOffset IssuedAt() => DateTimeOffset.UtcNow;

    public TimeSpan GetAccessTokenValidFor() => TimeSpan.FromMinutes(AccessTokenValidFor);
    public TimeSpan GetRefreshTokenValidFor() => TimeSpan.FromMinutes(RefreshTokenValidFor);

    public DateTimeOffset GetAccessTokenExpiration() => IssuedAt().Add(GetAccessTokenValidFor());
    public DateTimeOffset GetRefreshTokenExpiration() => IssuedAt().Add(GetRefreshTokenValidFor());

    public string GetJti() => Guid.NewGuid().ToString();

    public SymmetricSecurityKey GetSigningKey() => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SigningKey));
    public SigningCredentials GetSigningCredentials() => new SigningCredentials(GetSigningKey(), SecurityAlgorithms.HmacSha256);
}
