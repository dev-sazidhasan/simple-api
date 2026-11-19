using SimpleNameApi.DTOs.Requests;
using SimpleNameApi.DTOs.Responses;

namespace SimpleNameApi.Interfaces;

public interface IProductService
{
    ProductResponse Create(CreateProductRequest request);

    List<ProductResponse> GetAll();

    ProductResponse? GetById(Guid id);

    ProductResponse? Update(Guid id, UpdateProductRequest request);

    bool Delete(Guid id);
}