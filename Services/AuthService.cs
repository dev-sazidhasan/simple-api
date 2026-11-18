using Microsoft.AspNetCore.Identity;
using SimpleNameApi.DTOs.Requests;
using SimpleNameApi.DTOs.Responses;
using SimpleNameApi.Interfaces;
using SimpleNameApi.Models;

namespace SimpleNameApi.Services;

public class AuthService : IAuthService
{
private readonly IAuthRepository _authRepository;
private readonly ITokenService _tokenService;
private readonly PasswordHasher<User> _passwordHasher;

public AuthService(
    IAuthRepository authRepository,
    ITokenService tokenService)
{
    _authRepository = authRepository;
    _tokenService = tokenService;
    _passwordHasher = new PasswordHasher<User>();
}

    public AuthResponse Register(RegisterRequest request)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var existingUser = _authRepository.GetByEmail(email);

        if (existingUser is not null)
        {
            throw new InvalidOperationException(
                "Email is already registered."
            );
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName.Trim(),
            Email = email,
            CreatedAtUtc = DateTime.UtcNow
        };

        user.PasswordHash = _passwordHasher.HashPassword(
            user,
            request.Password
        );

        _authRepository.Add(user);

        return new AuthResponse
        {
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Message = "Registration successful."
        };
    }

public AuthResponse Login(LoginRequest request)
{
    var email = request.Email.Trim().ToLowerInvariant();

    var user = _authRepository.GetByEmail(email);

    if (user is null)
    {
        throw new UnauthorizedAccessException(
            "Invalid email or password."
        );
    }

    var verificationResult = _passwordHasher.VerifyHashedPassword(
        user,
        user.PasswordHash,
        request.Password
    );

    if (verificationResult == PasswordVerificationResult.Failed)
    {
        throw new UnauthorizedAccessException(
            "Invalid email or password."
        );
    }

    var accessToken = _tokenService.GenerateToken(user);

    return new AuthResponse
    {
        UserId = user.Id,
        FullName = user.FullName,
        Email = user.Email,
        AccessToken = accessToken,
        TokenType = "Bearer",
        ExpiresAtUtc = DateTime.UtcNow.AddMinutes(60),
        Message = "Login successful."
    };
}
}