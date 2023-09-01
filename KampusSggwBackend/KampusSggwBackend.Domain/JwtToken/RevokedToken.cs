namespace KampusSggwBackend.Domain.JwtToken;

using System;

public class RevokedToken
{
    public string Id { get; set; }
    public Guid UserAccountId { get; set; }

    public DateTimeOffset ExpirationTime { get; set; }
}
