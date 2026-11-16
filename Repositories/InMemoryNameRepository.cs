
using System.Collections.Concurrent;
using SimpleNameApi.Interfaces;
using SimpleNameApi.Models;

namespace SimpleNameApi.Repositories;

public class InMemoryNameRepository : INameRepository
{
    private readonly ConcurrentDictionary<Guid, Name> _names = new();

    public void Add(Name name)
    {
        bool added = _names.TryAdd(name.Id, name);

        if (!added)
        {
            throw new InvalidOperationException(
                "A name with the same ID already exists."
            );
        }
    }

    public List<Name> GetAll()
    {
        return _names.Values
            .OrderByDescending(name => name.CreatedAtUtc)
            .ToList();
    }
}
