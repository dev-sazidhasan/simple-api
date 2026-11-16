
using SimpleNameApi.DTOs.Requests;
using SimpleNameApi.DTOs.Responses;

namespace SimpleNameApi.Interfaces;

public interface INameService
{
    NameResponse CreateName(CreateNameRequest request);

    List<NameResponse> GetAllNames();

     NameResponse? GetNameById(Guid id);

    NameResponse? UpdateName(Guid id, UpdateNameRequest request);

    bool DeleteName(Guid id);
}
