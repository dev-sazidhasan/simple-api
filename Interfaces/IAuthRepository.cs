using SimpleNameApi.Models;

namespace SimpleNameApi.Interfaces;

public interface IAuthRepository
{
    void Add(User user);

    User? GetByEmail(string email);
}