namespace KampusSggwBackend.Domain.User;

using System;

public class UserEmailChange
{
    public Guid Id { get; set; }
    public string NewEmail { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
    public Guid UserId { get; set; }
    public string VerificationCode { get; set; }
}
