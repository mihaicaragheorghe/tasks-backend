using Microsoft.AspNetCore.Identity;
using Tasks.Api.Contracts;
using Application.Common.Models;
using Application.Common.Repository;
using Application.Core;
using Domain;

namespace Tasks.Api.Services;

public class AuthenticationService(
    IHttpContextAccessor httpContextAccessor,
    ILogger<AuthenticationService> logger,
    UserManager<User> userManager,
    TokenGeneratorService tokenGeneratorService,
    RefreshTokenValidator refreshTokenValidator,
    IRefreshTokenRepository refreshTokenRepository)
{
    public async Task<UserIdentityDto> Register(RegisterRequest request)
    {
        if (await userManager.FindByEmailAsync(request.Email) is not null)
        {
            throw new ServiceException(Errors.Authentication.EmailExists);
        }
        if (await userManager.FindByNameAsync(request.Username) is not null)
        {
            throw new ServiceException(Errors.Authentication.UsernameExists);
        }

        var user = User.Create(
            username: request.Username,
            email: request.Email,
            displayName: request.DisplayName);

        var accessToken = tokenGeneratorService.Generate(user);
        var refreshToken = tokenGeneratorService.GenerateRefreshToken();

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            throw new ServiceException(Errors.Authentication.FailedToCreateUser);
        }

        if (await refreshTokenRepository.AddAsync(new RefreshToken(user.Id, refreshToken)) <= 0)
        {
            logger.LogError("Failed to create refresh token for user {Username}", user.UserName);
        }

        return new UserIdentityDto(user, accessToken, refreshToken);
    }

    public async Task<UserIdentityDto> Login(LoginRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email) 
            ?? throw new ServiceException(Errors.Authentication.InvalidCredentials);

        if (!await userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new ServiceException(Errors.Authentication.InvalidCredentials);
        }

        var accessToken = tokenGeneratorService.Generate(user);
        var refreshToken = await refreshTokenRepository.GetAsync(user.Id);

        if (refreshToken is null)
        {
            refreshToken = new RefreshToken(user.Id, tokenGeneratorService.GenerateRefreshToken());
            
            if (await refreshTokenRepository.AddAsync(refreshToken) <= 0)
                logger.LogError("Failed to create refresh token for user {Username}", user.UserName);
        }
        else
        {
            refreshToken.Token = tokenGeneratorService.GenerateRefreshToken();

            if (await refreshTokenRepository.UpdateAsync(refreshToken) <= 0)
                logger.LogError("Failed to update refresh token for user {Username}", user.UserName);
        }

        return new UserIdentityDto(user, accessToken, refreshToken.Token);
    }

    public async Task<UserIdentityDto> RefreshToken(RefreshTokenRequest request)
    {
        var refreshToken = await refreshTokenRepository.GetAsync(request.RefreshToken)
            ?? throw new ServiceException(Errors.Authentication.RefreshTokenNotFound);

        if (!refreshTokenValidator.Validate(request.RefreshToken))
            throw new ServiceException(Errors.Authentication.InvalidRefreshToken);

        var userIdentity = await userManager.FindByIdAsync(refreshToken.UserId.ToString())
            ?? throw new ServiceException(Errors.Authentication.UserNotFound);

        var accessToken = tokenGeneratorService.Generate(userIdentity);
        refreshToken.Token = tokenGeneratorService.GenerateRefreshToken();

        bool success = await refreshTokenRepository.UpdateAsync(refreshToken) > 0;

        return success
            ? new UserIdentityDto(userIdentity, accessToken, refreshToken.Token)
            : throw new ServiceException(Errors.Authentication.FailedToCreateRefreshToken);
    }

    public async Task Logout()
    {
        var currentUser = httpContextAccessor.HttpContext?.User
            ?? throw new ServiceException(Errors.Authentication.Unauthorized);

        var userIdentity = await userManager.GetUserAsync(currentUser)
            ?? throw new ServiceException(Errors.Authentication.UserNotFound);

        var token = await refreshTokenRepository.GetAsync(userIdentity.Id)
            ?? throw new ServiceException(Errors.Authentication.RefreshTokenNotFound);

        if (await refreshTokenRepository.DeleteAsync(token) <= 0)
            logger.LogError("Failed to delete refresh token for user {Username}", userIdentity.UserName);
    }
}