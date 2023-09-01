namespace KampusSggwBackend.Controllers.Account.Parameters;

public class ResetPasswordParam
{
    public string Email { get; set; }
    public string NewPassword { get; set; }
    public string VerificationCode { get; set; }
}
