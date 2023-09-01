namespace KampusSggwBackend.Services.RequestingUser;

using KampusSggwBackend.Domain.RequestingUser;

public interface IRequestingUserService
{
    Task<RequestingUser> GetRequestingUser();
}
