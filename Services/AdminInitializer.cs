using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SimpleNameApi.Configurations;
using SimpleNameApi.Data;
using SimpleNameApi.Models;

namespace SimpleNameApi.Services;

public class AdminInitializer
{
    private readonly MongoDbContext _context;
    private readonly AdminSettings _settings;
    private readonly PasswordHasher<User> _passwordHasher;

    public AdminInitializer(
        MongoDbContext context,
        IOptions<AdminSettings> settings)
    {
        _context = context;
        _settings = settings.Value;
        _passwordHasher = new PasswordHasher<User>();
    }

    public void Initialize()
    {
        if (string.IsNullOrWhiteSpace(_settings.FullName) ||
            string.IsNullOrWhiteSpace(_settings.Email) ||
            string.IsNullOrWhiteSpace(_settings.Password))
        {
            throw new InvalidOperationException(
                "Admin configuration is incomplete."
            );
        }

        var existingUser = _context.Users
            .Find(user => user.Email == _settings.Email)
            .FirstOrDefault();

        if (existingUser != null)
        {
            if (existingUser.Role == "Admin")
            {
                return;
            }

            throw new InvalidOperationException(
                "The configured admin email already belongs to a non-admin user."
            );
        }

        var admin = new User
        {
            Id = Guid.NewGuid(),
            FullName = _settings.FullName,
            Email = _settings.Email,
            Role = "Admin",
            CreatedAtUtc = DateTime.UtcNow
        };

        admin.PasswordHash = _passwordHasher.HashPassword(
            admin,
            _settings.Password
        );

        _context.Users.InsertOne(admin);
    }
}