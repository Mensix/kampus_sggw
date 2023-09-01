namespace KampusSggwBackend.Infrastructure.UserService.Repositories.Users;

using KampusSggwBackend.Data;
using KampusSggwBackend.Domain.User;
using System;
using System.Linq;

public class UsersRepository : IUsersRepository
{
    // Services
    private readonly DataContext dbContext;

    // Constructor
    public UsersRepository(DataContext dbContext)
    {
        this.dbContext = dbContext;
    }

    // Methods
    public UserAccount Get(Guid id)
    {
        var user = dbContext.Users
            .Where(u => u.Id == id)
            .First();

        return user;
    }
}
