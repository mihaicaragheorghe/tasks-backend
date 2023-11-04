using Microsoft.AspNetCore.Identity;
using Tasks.Api.Contracts;
using Tasks.Application.Common.Models;
using Tasks.Application.Common.Repository;
using Tasks.Application.Core;
using Tasks.Domain;

namespace Tasks.Api.Services;

public class AuthenticationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<AuthenticationService> _logger;
    private readonly UserManager<User> _userManager;
    private readonly TokenGeneratorService _tokenGeneratorService;
    private readonly RefreshTokenValidator _refreshTokenValidator;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public AuthenticationService(
        IHttpContextAccessor httpContextAccessor,
        ILogger<AuthenticationService> logger,
        UserManager<User> userManager,
        TokenGeneratorService tokenGeneratorService,
        RefreshTokenValidator refreshTokenValidator,
        IRefreshTokenRepository refreshTokenRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _userManager = userManager;
        _tokenGeneratorService = tokenGeneratorService;
        _refreshTokenValidator = refreshTokenValidator;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<UserIdentityDto> Register(RegisterRequest request)
    {
        if (await _userManager.FindByEmailAsync(request.Email) is not null)
        {
            throw new ServiceException(Errors.Authentication.EmailExists);
        }
        if (await _userManager.FindByNameAsync(request.Username) is not null)
        {
            throw new ServiceException(Errors.Authentication.UsernameExists);
        }

        var user = User.Create(
            username: request.Username,
            email: request.Email,
            displayName: request.DisplayName);

        var accessToken = _tokenGeneratorService.Generate(user);
        var refreshToken = _tokenGeneratorService.GenerateRefreshToken();

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            throw new ServiceException(Errors.Authentication.FailedToCreateUser);
        }

        if (await _refreshTokenRepository.AddAsync(new RefreshToken(user.Id, refreshToken)) <= 0)
        {
            _logger.LogError("Failed to create refresh token for user {Username}", user.UserName);
        }

        return new UserIdentityDto(user, accessToken, refreshToken);
    }

    public async Task<UserIdentityDto> Login(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email) 
            ?? throw new ServiceException(Errors.Authentication.InvalidCredentials);

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new ServiceException(Errors.Authentication.InvalidCredentials);
        }

        var accessToken = _tokenGeneratorService.Generate(user);
        var refreshToken = await _refreshTokenRepository.GetAsync(user.Id);

        if (refreshToken is null)
        {
            refreshToken = new RefreshToken(user.Id, _tokenGeneratorService.GenerateRefreshToken());
            
            if (await _refreshTokenRepository.AddAsync(refreshToken) <= 0)
                _logger.LogError("Failed to create refresh token for user {Username}", user.UserName);
        }
        else
        {
            refreshToken.Token = _tokenGeneratorService.GenerateRefreshToken();

            if (await _refreshTokenRepository.UpdateAsync(refreshToken) <= 0)
                _logger.LogError("Failed to update refresh token for user {Username}", user.UserName);
        }

        return new UserIdentityDto(user, accessToken, refreshToken.Token);
    }

    public async Task<UserIdentityDto> RefreshToken(RefreshTokenRequest request)
    {
        var refreshToken = await _refreshTokenRepository.GetAsync(request.RefreshToken)
            ?? throw new ServiceException(Errors.Authentication.RefreshTokenNotFound);

        if (!_refreshTokenValidator.Validate(request.RefreshToken))
            throw new ServiceException(Errors.Authentication.InvalidRefreshToken);

        var userIdentity = await _userManager.FindByIdAsync(refreshToken.UserId.ToString())
            ?? throw new ServiceException(Errors.Authentication.UserNotFound);

        var accessToken = _tokenGeneratorService.Generate(userIdentity);
        refreshToken.Token = _tokenGeneratorService.GenerateRefreshToken();

        bool success = await _refreshTokenRepository.UpdateAsync(refreshToken) > 0;

        return success
            ? new UserIdentityDto(userIdentity, accessToken, refreshToken.Token)
            : throw new ServiceException(Errors.Authentication.FailedToCreateRefreshToken);
    }

    public async Task Logout()
    {
        var currentUser = _httpContextAccessor.HttpContext?.User
            ?? throw new ServiceException(Errors.Authentication.Unauthorized);

        var userIdentity = await _userManager.GetUserAsync(currentUser)
            ?? throw new ServiceException(Errors.Authentication.UserNotFound);

        var token = await _refreshTokenRepository.GetAsync(userIdentity.Id)
            ?? throw new ServiceException(Errors.Authentication.RefreshTokenNotFound);

        if (await _refreshTokenRepository.DeleteAsync(token) <= 0)
            _logger.LogError("Failed to delete refresh token for user {Username}", userIdentity.UserName);
    }
}