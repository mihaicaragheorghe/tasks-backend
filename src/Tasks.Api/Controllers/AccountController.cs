using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tasks.Api.Contracts;
using Tasks.Api.Services;
using Tasks.Application.Core;
using Tasks.Domain;
using AppUser = Tasks.Domain.User;

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

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterRequest request)
    {
        if (await _userManager.FindByEmailAsync(request.Email) is not null)
        {
            throw new AppException(Errors.Authentication.EmailExists);
        }
        if (await _userManager.FindByNameAsync(request.Username) is not null)
        {
            throw new AppException(Errors.Authentication.UsernameExists);
        }

        var user = AppUser.Create(
            username: request.Username,
            email: request.Email,
            displayName: request.DisplayName,
            refreshToken: _tokenGeneratorService.GenerateRefreshToken());

        var accessToken = _tokenGeneratorService.Generate(user);

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            throw new AppException(Errors.Authentication.FailedToCreateUser);
        }

        return new UserDto(user, accessToken, user.RefreshToken!);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginRequest request)
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

        return new UserDto(user, token, refreshToken);
    }

    [Authorize]
    [HttpPost("refresh-token")]
    public async Task<ActionResult<UserDto>> RefreshToken(RefreshTokenRequest request)
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

        return new UserDto(user, token, refreshToken);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user is null) return Unauthorized();

        user = user.Update(refreshToken: null);
        var result = await _userManager.UpdateAsync(user);

        return Ok();
    }
}