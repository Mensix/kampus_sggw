namespace KampusSggwBackend.Infrastructure.UserService.Repositories.Users;

using KampusSggwBackend.Domain.User;
using System;

public interface IUsersRepository
{
    UserAccount Get(Guid id);
}
