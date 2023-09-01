namespace KampusSggwBackend.Domain.User;

using Microsoft.AspNetCore.Identity;
using System;

public class UserAccount : IdentityUser<Guid>
{
    public UserLanguage Language { get; set; }
    public bool WelcomeEmailSent { get; set; }
    public string DeviceToken { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public DateTimeOffset LastActiveAt { get; set; }
}