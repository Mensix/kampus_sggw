namespace KampusSggwBackend.Infrastructure.UserService.Repositories.VerificationCodes;

using KampusSggwBackend.Domain.User;
using System;

public interface IVerificationCodesRepository
{
    VerificationCode Get(string id);
    VerificationCode GetLast(Guid userId);
    void Create(VerificationCode verificationCode);
    void Delete(VerificationCode verificationCode);
}
