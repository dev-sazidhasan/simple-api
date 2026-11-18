using MongoDB.Driver;
using SimpleNameApi.Data;
using SimpleNameApi.Interfaces;
using SimpleNameApi.Models;

namespace SimpleNameApi.Repositories;

public class MongoAuthRepository : IAuthRepository
{
    private readonly MongoDbContext _context;

    public MongoAuthRepository(MongoDbContext context)
    {
        _context = context;
    }

    public void Add(User user)
    {
        _context.Users.InsertOne(user);
    }

    public User? GetByEmail(string email)
    {
        return _context.Users
            .Find(user => user.Email == email)
            .FirstOrDefault();
    }
}