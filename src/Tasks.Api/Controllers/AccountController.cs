using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tasks.Api.Contracts;
using Tasks.Api.Services;
using Tasks.Application.Core;
using Tasks.Domain;

namespace Tasks.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly TokenGeneratorService _tokenGeneratorService;
    private readonly RefreshTokenValidator _refreshTokenValidator;

    public AccountController(
        UserManager<User> userManager,
        TokenGeneratorService tokenGeneratorService,
        RefreshTokenValidator refreshTokenValidator)
    {
        _userManager = userManager;
        _tokenGeneratorService = tokenGeneratorService;
        _refreshTokenValidator = refreshTokenValidator;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null) return Unauthorized();

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
            return Unauthorized();

        var token = _tokenGeneratorService.Generate(user);
        var refreshToken = _tokenGeneratorService.GenerateRefreshToken();

        user = user.Update(refreshToken: refreshToken);
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded) 
            throw new AppException(Errors.Authentication.FailedToCreateRefreshToken);

        return new LoginResponse(user, token, refreshToken);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<RefreshTokenResponse>> RefreshToken(RefreshTokenRequest request)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user is null) return Unauthorized();

        if (user.RefreshToken != request.RefreshToken) return Unauthorized();

        if (!_refreshTokenValidator.Validate(request.RefreshToken)) return Unauthorized();

        var token = _tokenGeneratorService.Generate(user);
        var refreshToken = _tokenGeneratorService.GenerateRefreshToken();

        user = user.Update(refreshToken: refreshToken);
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded) 
            throw new AppException(Errors.Authentication.FailedToCreateRefreshToken);

        return new RefreshTokenResponse(token, refreshToken);
    }
}