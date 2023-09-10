namespace KampusSggwBackend.Infrastructure.UserService.Repositories.UserEmailChanges;

using KampusSggwBackend.Domain.User;
using System;

public interface IUserEmailChangesRepository
{
    UserEmailChange Get(Guid userId);
    void Create(UserEmailChange userEmailChange);
    void Update(UserEmailChange userEmailChange);
}
