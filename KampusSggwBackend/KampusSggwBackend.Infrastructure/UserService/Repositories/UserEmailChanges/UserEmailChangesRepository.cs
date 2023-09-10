namespace KampusSggwBackend.Infrastructure.UserService.Repositories.UserEmailChanges;

using KampusSggwBackend.Data;
using KampusSggwBackend.Domain.User;
using System;
using System.Linq;

public class UserEmailChangesRepository : IUserEmailChangesRepository
{
    // Services
    private readonly DataContext dbContext;

    // Constructor
    public UserEmailChangesRepository(DataContext dbContext)
    {
        this.dbContext = dbContext;
    }

    // Methods
    public UserEmailChange Get(Guid userId)
    {
        return dbContext.UserEmailChanges.FirstOrDefault(uec => uec.UserId == userId);
    }

    public void Create(UserEmailChange userEmailChange)
    {
        dbContext.Add(userEmailChange);
        dbContext.SaveChanges();
    }

    public void Update(UserEmailChange userEmailChange)
    {
        dbContext.Update(userEmailChange);
        dbContext.SaveChanges();
    }
}
