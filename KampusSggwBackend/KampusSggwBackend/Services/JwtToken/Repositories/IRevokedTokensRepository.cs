namespace KampusSggwBackend.Services.JwtToken.Repositories;

using KampusSggwBackend.Domain.JwtToken;

public interface IRevokedTokensRepository
{
    RevokedToken Get(string tokenId);
    void Create(RevokedToken revokedToken);
}
