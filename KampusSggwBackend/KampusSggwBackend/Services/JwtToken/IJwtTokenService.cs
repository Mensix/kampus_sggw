namespace KampusSggwBackend.Services.JwtToken;

using KampusSggwBackend.Domain.JwtToken;

public interface IJwtTokenService
{
    JwtTokens CreateTokens(Guid userId);
    RefreshToken ReadRefreshToken(string refreshToken);
}
