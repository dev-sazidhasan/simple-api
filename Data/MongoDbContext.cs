
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using SimpleNameApi.Configurations;
using SimpleNameApi.Models;

namespace SimpleNameApi.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDBSettings> settings)
    {
        var mongoSettings = settings.Value;

        var mongoClient = new MongoClient(
            mongoSettings.ConnectionString
        );

        _database = mongoClient.GetDatabase(
            mongoSettings.DatabaseName
        );

        Names = _database.GetCollection<Name>(
            mongoSettings.CollectionName
        );
    }

    public IMongoCollection<Name> Names { get; }
}
