using SimpleNameApi.Models;

namespace SimpleNameApi.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}