namespace KampusSggwBackend.Infrastructure.UserService;

using KampusSggwBackend.Domain.User;
using KampusSggwBackend.Infrastructure.UserService.Repositories.VerificationCodes;
using System;
using System.Threading.Tasks;

public class VerificationCodeFactoryService : IVerificationCodeFactoryService
{
    // Consts
    private TimeSpan verificationCodeLifetime = TimeSpan.FromHours(4);

    // Services
    private readonly Random random;
    private readonly IVerificationCodesRepository verificationCodesRepository;

    // Constructor
    public VerificationCodeFactoryService(IVerificationCodesRepository verificationCodesRepository)
    {
        this.random = new Random();
        this.verificationCodesRepository = verificationCodesRepository;
    }

    // Methods
    public async Task<VerificationCode> CreateVerificationCode(Guid userId)
    {
        var verificationCode = new VerificationCode()
        {
            Id = Guid.NewGuid().ToString(),
            UserId = userId,
            ActiveUntil = DateTimeOffset.UtcNow + verificationCodeLifetime,
            Value = random.Next(100000, 999999).ToString(), // 6 digit number
        };

        verificationCodesRepository.Create(verificationCode);

        return verificationCode;
    }

    public async Task<VerificationCode> GetLastUserVerificationCode(Guid userId)
    {
        var verificationCode = verificationCodesRepository.GetLast(userId);
        return verificationCode;
    }

    public async Task RemoveVerificationCode(VerificationCode verificationCode)
    {
        verificationCodesRepository.Delete(verificationCode);
    }
}
