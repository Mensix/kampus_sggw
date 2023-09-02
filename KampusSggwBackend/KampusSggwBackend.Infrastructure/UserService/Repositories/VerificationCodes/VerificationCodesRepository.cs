namespace KampusSggwBackend.Infrastructure.UserService.Repositories.VerificationCodes;

using KampusSggwBackend.Data;
using KampusSggwBackend.Domain.User;
using System;
using System.Linq;

public class VerificationCodesRepository : IVerificationCodesRepository
{
    // Services
    private readonly DataContext dbContext;

    // Constructor
    public VerificationCodesRepository(DataContext dbContext)
    {
        this.dbContext = dbContext;
    }

    // Methods
    public VerificationCode Get(string id)
    {
        var verificationCode = dbContext.VerificationCodes.First(u => u.Id == id);
        return verificationCode;
    }

    public VerificationCode GetLast(Guid userId)
    {
        var verificationCode = dbContext.VerificationCodes
            .Where(vc => vc.UserId == userId && vc.ConfirmedForPasswordReset == false)
            .OrderByDescending(vc => vc.ActiveUntil)
            .FirstOrDefault();

        return verificationCode;
    }

    public void Create(VerificationCode verificationCode)
    {
        dbContext.VerificationCodes.Add(verificationCode);
        dbContext.SaveChanges();
    }

    public void Delete(VerificationCode verificationCode)
    {
        dbContext.VerificationCodes.Remove(verificationCode);
        dbContext.SaveChanges();
    }
}
