namespace KampusSggwBackend.Domain.User;

using System;

public class VerificationCode
{
    public string Id { get; set; }
    public DateTimeOffset ActiveUntil { get; set; }
    public Guid UserId { get; set; }
    public string Value { get; set; }
    public bool ConfirmedForPasswordReset { get; set; }
    
    //public VerificationCode(Guid userAccountId)
    //{
    //    Id = Guid.NewGuid().ToString();
    //    UserId = userAccountId;

    //    var random = new Random();
    //    Value = random.Next(100000, 999999).ToString();
    //}
}
