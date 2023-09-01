namespace KampusSggwBackend.Services.JwtToken;

using KampusSggwBackend.Configuration;
using KampusSggwBackend.Domain.JwtToken;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class JwtTokenService : IJwtTokenService
{
    // Services
    private readonly JwtSettings jwtSettings;
    private readonly JwtSecurityTokenHandler securityTokenHandler;

    // Constructor
    public JwtTokenService(JwtSettings jwtSettings)
    {
        this.jwtSettings = jwtSettings;
        securityTokenHandler = new JwtSecurityTokenHandler();
    }

    // Methods
    public RefreshToken ReadRefreshToken(string refreshToken)
    {
        var token = new JwtSecurityTokenHandler().ReadJwtToken(refreshToken);

        var userId = new Guid(token.Claims.First(c => c.Type == JwtRegisteredClaimNames.NameId).Value);
        var tokenRoleClaim = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
        var expirationClaim = token.Claims.First(c => c.Type == JwtRegisteredClaimNames.Exp);
        var expirationTime = DateTimeOffset.FromUnixTimeSeconds(int.Parse(expirationClaim.Value)).DateTime;

        var result = new RefreshToken()
        {
            UserId = userId,
            TokenId = token.Id,
            Expiration = expirationTime,
            TokenRole = tokenRoleClaim.Value,
        };

        return result;
    }

    public JwtTokens CreateTokens(Guid userId)
    {
        var accessToken = IssueToken(CreateClaims(userId, "AccessToken"));
        var refreshToken = IssueToken(CreateClaims(userId, "RefreshToken"), true);

        var jwtTokens = new JwtTokens()
        {
            AccessToken = accessToken,
            RefrehToken = refreshToken,
        };

        return jwtTokens;
    }

    private string IssueToken(IEnumerable<Claim> userClaims, bool isRefreshToken = false)
    {
        var claims = CreateMandatoryUserClaims();

        claims.AddRange(userClaims);

        var tokenExpiration = isRefreshToken
            ? jwtSettings.GetRefreshTokenExpiration()
            : jwtSettings.GetAccessTokenExpiration();

        var jwt = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            notBefore: JwtSettings.NotBefore(),
            expires: tokenExpiration.UtcDateTime,
            signingCredentials: jwtSettings.GetSigningCredentials());

        return securityTokenHandler.WriteToken(jwt);
    }

    private static IEnumerable<Claim> CreateClaims(Guid userId, params string[] roleValues)
    {
        return roleValues.Select(roleValue => new Claim(ClaimTypes.Role, roleValue))
                         .Append(new Claim(JwtRegisteredClaimNames.NameId, userId.ToString("N")));
    }

    private List<Claim> CreateMandatoryUserClaims()
    {
        return new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, jwtSettings.GetJti()),
            new Claim(JwtRegisteredClaimNames.Iat, JwtSettings.IssuedAt().ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        };
    }
}
