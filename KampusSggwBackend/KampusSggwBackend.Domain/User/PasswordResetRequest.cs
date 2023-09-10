namespace KampusSggwBackend.Domain.User;

using System;

public class PasswordResetRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTimeOffset ActiveUntil { get; set; }
    public string Value { get; set; }
    public int InvalidAttempts { get; set; }
}
