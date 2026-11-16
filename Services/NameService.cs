
using SimpleNameApi.DTOs.Requests;
using SimpleNameApi.DTOs.Responses;
using SimpleNameApi.Interfaces;
using SimpleNameApi.Models;

namespace SimpleNameApi.Services;

public class NameService : INameService
{
    private readonly INameRepository _nameRepository;

    public NameService(INameRepository nameRepository)
    {
        _nameRepository = nameRepository;
    }

    public NameResponse CreateName(CreateNameRequest request)
    {
        var name = new Name
        {
            Id = Guid.NewGuid(),
            NameValue = request.Name.Trim(),
            CreatedAtUtc = DateTime.UtcNow
        };

        _nameRepository.Add(name);

        return MapToResponse(name);
    }

    public List<NameResponse> GetAllNames()
    {
        var names = _nameRepository.GetAll();

        return names
            .Select(MapToResponse)
            .ToList();
    }

    public NameResponse? GetNameById(Guid id)
    {
        var name = _nameRepository.GetById(id);

        if (name is null)
        {
            return null;
        }

        return MapToResponse(name);
    }

    public NameResponse? UpdateName(
        Guid id,
        UpdateNameRequest request)
    {
        var existingName = _nameRepository.GetById(id);

        if (existingName is null)
        {
            return null;
        }

        existingName.NameValue = request.Name.Trim();

        bool updated = _nameRepository.Update(existingName);

        if (!updated)
        {
            return null;
        }

        return MapToResponse(existingName);
    }

    public bool DeleteName(Guid id)
    {
        var existingName = _nameRepository.GetById(id);

        if (existingName is null)
        {
            return false;
        }

        return _nameRepository.Delete(id);
    }

    private static NameResponse MapToResponse(Name name)
    {
        return new NameResponse
        {
            Id = name.Id,
            Name = name.NameValue,
            CreatedAtUtc = name.CreatedAtUtc
        };
    }
}
