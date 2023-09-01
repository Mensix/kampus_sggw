namespace KampusSggwBackend.Controllers.Account;

using KampusSggwBackend.Controllers.Account.Parameters;
using KampusSggwBackend.Domain.Exceptions;
using KampusSggwBackend.Domain.User;
using KampusSggwBackend.Infrastructure.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    // Services
    private readonly UserManager<UserAccount> userManager;
    // private readonly IEmailService _emailService;
    private readonly IVerificationCodeFactoryService verificationCodeFactoryService;

    // Constructor
    public AccountController(
        UserManager<UserAccount> _userManager,
        IVerificationCodeFactoryService verificationCodeFactoryService)
    {
        this.userManager = _userManager;
        this.verificationCodeFactoryService = verificationCodeFactoryService;
    }

    // Methods
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult> CreateAccount([FromBody] CreateAccountParam param)
    {
        var user = new UserAccount()
        {
            Email = param.Email,
            UserName = param.Email,
            Language = UserLanguage.Polish,
            LastActiveAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        var result = await userManager.CreateAsync(user, param.Password);

        if (!result.Succeeded)
        {
            throw new AppException($"Code: {result.Errors.First().Code},\nDescription: {result.Errors.First().Description}");
        }

        var verificationCode = await verificationCodeFactoryService.CreateVerificationCode(user.Id);

        //await _emailService.SendEmailConfirmationForNewAccount(
        //    userAccount.Email, 
        //    emailVerificationCode.Value, 
        //    userAccount.Language);

        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("test")]
    public async Task<ActionResult> CreateTestAccount([FromBody] CreateTestAccountParam param)
    {
        if (param.SuperPassword != "0d2f3476-5c21-4a49-82e4-480f590ed298")
        {
            return Forbid();
        }

        var user = new UserAccount()
        {
            Email = param.Email,
            UserName = param.Email,
            Language = UserLanguage.Polish,
            LastActiveAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        var result = await userManager.CreateAsync(user, param.Password);

        if (!result.Succeeded)
        {
            throw new AppException($"Code: {result.Errors.First().Code},\nDescription: {result.Errors.First().Description}");
        }

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        await userManager.ConfirmEmailAsync(user, token);

        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("confirm")]
    public async Task<ActionResult> ConfirmAccount([FromBody] ConfirmAccountParam param)
    {
        var user = userManager.Users.FirstOrDefault(u => u.Email == param.Email);

        if (user == null)
        {
            return NotFound("User not found");
        }

        var verificationCode = await verificationCodeFactoryService.GetLastUserVerificationCode(user.Id);

        if (verificationCode == null)
        {
            return NotFound("Account is already confirmed"); 
        }

        if (verificationCode.Value != param.VerificationCode)
        {
            return BadRequest("Confirmation code is invalid");
        }

        if (verificationCode!.ActiveUntil < DateTimeOffset.Now)
        {
            return BadRequest("Confirmation code has expired");
        }

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        await userManager.ConfirmEmailAsync(user, token);

        await verificationCodeFactoryService.RemoveVerificationCode(verificationCode);

        return Ok();
    }

    //[AllowAnonymous]
    //[HttpPost("reset-password")]
    //public Task<ActionResult> ResetPassword([FromBody] ResetPasswordParam param)
    //{
    //    var user =  await userManager.Users
    //                    .Where(u => u.Email == param.Email)
    //                    .Include(u => u.PasswordResetRequests)
    //                    .FirstOrDefaultAsync(ct);

    //    if (user == null)
    //    {
    //        return NotFound("User not found");
    //    }

    //    var passwordResetRequest = user.PasswordResetRequests
    //        .FirstOrDefault(r => r.Value == command.VerificationCode);

    //    if (passwordResetRequest == null)
    //    {
    //        await LogInvalidPasswordResetRequestAttempt(userAccount.PasswordResetRequests, ct);
    //        throw ExceptionFactory.Create(new InvalidConfirmationCodeError());
    //    }

    //    if (passwordResetRequest.InvalidAttempts > 2)
    //    {
    //        throw ExceptionFactory.Create(new ExpiredConfirmationCodeError());
    //        //throw ExceptionFactory.Create(new TooManyResetPasswordWithInvalidCodeAttemptsCodeError());
    //    }

    //    if (passwordResetRequest.ActiveUntil < DateTimeOffset.UtcNow)
    //    {
    //        throw ExceptionFactory.Create(new ExpiredConfirmationCodeError());
    //    }

    //    var token = await _userManager.GeneratePasswordResetTokenAsync(userAccount);

    //    var result = await _userManager.ResetPasswordAsync(userAccount, token, command.NewPassword);

    //    if (!result.Succeeded)
    //    {
    //        throw ExceptionFactory.Create(new CustomError
    //        {
    //            Code = result.Errors.First().Code,
    //            Description = result.Errors.First().Description
    //        });
    //    }

    //    _db.PasswordResetRequests.RemoveRange(userAccount.PasswordResetRequests);
    //    await _db.SaveChangesAsync(ct);

    //    return Mediator.Send(command);
    //}
}
