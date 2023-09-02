namespace KampusSggwBackend.Controllers.Users;

using KampusSggwBackend.Controllers.Users.Parameters;
using KampusSggwBackend.Controllers.Users.Results;
using KampusSggwBackend.Domain.JwtToken;
using KampusSggwBackend.Domain.User;
using KampusSggwBackend.Services.JwtToken;
using KampusSggwBackend.Services.JwtToken.Repositories;
using KampusSggwBackend.Services.RequestingUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class TokensController : ControllerBase
{
    // Services
    private readonly IRequestingUserService requestingUserService;
    private readonly UserManager<UserAccount> userManager;
    private readonly SignInManager<UserAccount> signInManager;
    private readonly IJwtTokenService jwtService;
    private readonly IRevokedTokensRepository revokedTokensRepository;

    // Constructor
    public TokensController(
        IRequestingUserService requestingUserService,
        UserManager<UserAccount> userManager,
        SignInManager<UserAccount> signInManager,
        IJwtTokenService jwtService,
        IRevokedTokensRepository revokedTokensRepository)
    {
        this.requestingUserService = requestingUserService;
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.jwtService = jwtService;
        this.revokedTokensRepository = revokedTokensRepository;
    }

    // Methods
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<LoginResult>> Login([FromBody] LoginParam param)
    {
        var user = await userManager.Users.SingleOrDefaultAsync(x => x.NormalizedEmail == param.Email);

        if (user == null)
        {
            return NotFound("Invalid credentials");
        }

        // Check if user has correct password without checking is acc is active
        var isLogged = await signInManager.UserManager.CheckPasswordAsync(user, password: param.Password);
        if (isLogged)
        {
            if (!user.EmailConfirmed)
            {
                return NotFound("Email not confirmed");
            }
        }

        var signInResult = await signInManager.CheckPasswordSignInAsync(user, param.Password, true);
        
        if (!signInResult.Succeeded)
        {
            return NotFound("Invalid credentials");
        }

        user.LastActiveAt = DateTimeOffset.UtcNow;
        if (param.DeviceToken != null)
        {
            var users = userManager.Users.Where(u => 
                u.DeviceToken == param.DeviceToken && 
                u.Email != param.Email &&
                u.DeviceToken != null && 
                u.DeviceToken != "string")
                .ToList();

            foreach (var u in users)
            {
                u.DeviceToken = "";
                await userManager.UpdateAsync(u);
            }
        }

        if (!string.IsNullOrWhiteSpace(param.DeviceToken))
        {
            user.DeviceToken = param.DeviceToken;
        }

        await userManager.UpdateAsync(user);

        var tokens = jwtService.CreateTokens(user.Id);

        return new LoginResult()
        {
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefrehToken,
        };
    }

    [AllowAnonymous]
    [HttpPost("refresh")]
    public async Task<ActionResult<RefreshTokenResult>> RefreshToken([FromBody] RefreshTokenParam param)
    {
        var refreshToken = jwtService.ReadRefreshToken(param.RefreshToken);

        var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == refreshToken.UserId);

        if (user == null)
        {
            return BadRequest("Invalid refresh token");
        }

        var revokedToken = revokedTokensRepository.Get(refreshToken.TokenId);

        if (revokedToken != null)
        {
            return BadRequest("Refresh token already revoked");
        }

        revokedToken = new RevokedToken
        {
            Id = refreshToken.TokenId,
            ExpirationTime = refreshToken.Expiration,
        };

        revokedTokensRepository.Create(revokedToken);
        user.LastActiveAt = DateTimeOffset.UtcNow;

        var newTokens = jwtService.CreateTokens(user.Id);

        var result = new RefreshTokenResult
        {
            AccessToken = newTokens.AccessToken,
            RefreshToken = newTokens.RefrehToken,
        };

        return Ok(result);
    }

    [Authorize]
    [HttpDelete("revoke")]
    public async Task<ActionResult> RevokeToken([FromBody] RevokeTokenParam param)
    {
        var refreshToken = jwtService.ReadRefreshToken(param.RefreshToken);
        var requestingUser = await requestingUserService.GetRequestingUser();

        // Refresh token to cancel must belong to user
        if (requestingUser.Id != refreshToken.UserId)
        {
            return BadRequest("Invalid refresh token");
        }

        var revokedToken = revokedTokensRepository.Get(refreshToken.TokenId);

        // If refresh token already revoked
        if (revokedToken != null)
        {
            return BadRequest("Refresh token already revoked");
        }

        revokedToken = new RevokedToken()
        {
            Id = refreshToken.TokenId,
            UserAccountId = requestingUser.Id,
            ExpirationTime = refreshToken.Expiration
        };
        revokedTokensRepository.Create(revokedToken);

        return Ok();
    }
}
