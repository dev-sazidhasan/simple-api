using SimpleNameApi.DTOs.Requests;
using SimpleNameApi.DTOs.Responses;

namespace SimpleNameApi.Interfaces;

public interface IAuthService
{
    AuthResponse Register(RegisterRequest request);

    AuthResponse Login(LoginRequest request);
}