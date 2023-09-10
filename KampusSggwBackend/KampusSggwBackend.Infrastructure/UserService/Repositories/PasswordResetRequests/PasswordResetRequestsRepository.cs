namespace KampusSggwBackend.Infrastructure.UserService.Repositories.PasswordResetRequests;

using KampusSggwBackend.Data;
using KampusSggwBackend.Domain.User;
using System;
using System.Linq;

public class PasswordResetRequestsRepository : IPasswordResetRequestsRepository
{
    // Services
    private readonly DataContext dbContext;

    // Constructor
    public PasswordResetRequestsRepository(DataContext dbContext)
    {
        this.dbContext = dbContext;
    }

    // Methods
    public PasswordResetRequest Get(string value)
    {
        return dbContext.PasswordResetRequests.FirstOrDefault(prr => prr.Value == value);
    }

    public void LogInvalidPasswordResetRequestForUser(Guid id)
    {
        var userPasswordRequests = dbContext.PasswordResetRequests.Where(prr =>  prr.UserId == id);

        foreach (var request in userPasswordRequests)
        {
            request.InvalidAttempts++;
        }

        dbContext.SaveChangesAsync();
    }

    public void DeleteForUser(Guid id)
    {
        var userPasswordRequests = dbContext.PasswordResetRequests.Where(prr => prr.UserId == id);
        dbContext.Remove(userPasswordRequests);
        dbContext.SaveChanges();
    }
}
