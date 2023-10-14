using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasks.Api.Contracts;
using Tasks.Api.Services;

namespace Tasks.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly AuthenticationService _authenticationService;

    public AccountController(AuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<UserIdentityDto>> Register(RegisterRequest request)
    {
        return await _authenticationService.Register(request);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<UserIdentityDto>> Login(LoginRequest request)
    {
        return await _authenticationService.Login(request);
    }

    [Authorize]
    [HttpPost("refresh-token")]
    public async Task<ActionResult<UserIdentityDto>> RefreshToken(RefreshTokenRequest request)
    {
        return await _authenticationService.RefreshToken(request);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await _authenticationService.Logout();
        return Ok();
    }
}