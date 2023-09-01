namespace KampusSggwBackend.Services.JwtToken.Repositories;

using KampusSggwBackend.Data;
using KampusSggwBackend.Domain.JwtToken;
using System.Linq;

public class RevokedTokensRepository : IRevokedTokensRepository
{
    // Services
    public DataContext DataContext { get; set; }

    // Constructor
    public RevokedTokensRepository(DataContext dataContext)
    {
        this.DataContext = dataContext;
    }

    // Methods
    public RevokedToken Get(string tokenId)
    {
        return DataContext.RevokedTokens.FirstOrDefault(t => t.Id == tokenId);
    }

    public void Create(RevokedToken revokedToken)
    {
        DataContext.Add(revokedToken);
        DataContext.SaveChanges();
    }
}
