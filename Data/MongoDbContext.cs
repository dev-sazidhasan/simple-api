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

        Products = _database.GetCollection<Product>("Products");
        
        Orders = _database.GetCollection<Order>("Orders");
    }

    public IMongoCollection<Name> Names { get; }

    public IMongoCollection<User> Users { get; }
    public IMongoCollection<Product> Products { get; }
    public IMongoCollection<Order> Orders { get; }
}