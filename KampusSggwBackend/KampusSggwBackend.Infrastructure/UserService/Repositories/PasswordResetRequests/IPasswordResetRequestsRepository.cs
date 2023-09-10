namespace KampusSggwBackend.Infrastructure.UserService.Repositories.PasswordResetRequests;

using KampusSggwBackend.Domain.User;
using System;

public interface IPasswordResetRequestsRepository
{
    PasswordResetRequest Get(string value);
    void LogInvalidPasswordResetRequestForUser(Guid id);
    void DeleteForUser(Guid id);
}
