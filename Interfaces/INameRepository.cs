
using SimpleNameApi.Models;

namespace SimpleNameApi.Interfaces;

public interface INameRepository
{
    void Add(Name name);

    List<Name> GetAll();
    Name? GetById(Guid id);

    bool Update(Name name);

    bool Delete(Guid id);
}
