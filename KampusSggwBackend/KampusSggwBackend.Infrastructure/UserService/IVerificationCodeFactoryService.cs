namespace KampusSggwBackend.Infrastructure.UserService;

using KampusSggwBackend.Domain.User;
using System.Threading.Tasks;

public interface IVerificationCodeFactoryService
{
    Task<VerificationCode> CreateVerificationCode(Guid userId);
    Task<VerificationCode> GetLastUserVerificationCode(Guid userId);
    Task RemoveVerificationCode(VerificationCode verificationCode);
}
