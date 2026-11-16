
using SimpleNameApi.Models;

namespace SimpleNameApi.Interfaces;

public interface INameRepository
{
    void Add(Name name);

    List<Name> GetAll();
}
