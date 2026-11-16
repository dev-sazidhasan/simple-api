
using MongoDB.Driver;
using SimpleNameApi.Data;
using SimpleNameApi.Interfaces;
using SimpleNameApi.Models;

namespace SimpleNameApi.Repositories;

public class MongoNameRepository : INameRepository
{
    private readonly MongoDbContext _context;

    public MongoNameRepository(MongoDbContext context)
    {
        _context = context;
    }

    public void Add(Name name)
    {
        _context.Names.InsertOne(name);
    }

    public List<Name> GetAll()
    {
        return _context.Names
            .Find(_ => true)
            .SortByDescending(name => name.CreatedAtUtc)
            .ToList();
    }

    public Name? GetById(Guid id)
    {
        Console.WriteLine($"Requested ID: {id}");

        var name = _context.Names
            .Find(x => x.Id == id)
            .FirstOrDefault();

        Console.WriteLine(
            $"Found Name: {name?.NameValue ?? "NOT FOUND"}"
        );

        return name;
    }

    public bool Update(Name name)
    {
        var filter = Builders<Name>.Filter
            .Eq(x => x.Id, name.Id);

        var update = Builders<Name>.Update
            .Set(x => x.NameValue, name.NameValue);

        var result = _context.Names.UpdateOne(filter, update);

        return result.MatchedCount > 0;
    }

    public bool Delete(Guid id)
    {
        var filter = Builders<Name>.Filter
            .Eq(x => x.Id, id);

        var result = _context.Names.DeleteOne(filter);

        return result.DeletedCount > 0;
    }
}
