using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SimpleNameApi.Configurations;
using SimpleNameApi.Models;

namespace SimpleNameApi.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDBSettings> settings)
    {
        var mongoClient = new MongoClient(
            settings.Value.ConnectionString
        );

        _database = mongoClient.GetDatabase(
            settings.Value.DatabaseName
        );

        Names = _database.GetCollection<Name>(
            settings.Value.CollectionName
        );

        Users = _database.GetCollection<User>("Users");
    }

    public IMongoCollection<Name> Names { get; }

    public IMongoCollection<User> Users { get; }
}