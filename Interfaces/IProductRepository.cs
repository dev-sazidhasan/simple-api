using SimpleNameApi.Models;

namespace SimpleNameApi.Interfaces;

public interface IProductRepository
{
    void Add(Product product);

    List<Product> GetAll();

    Product? GetById(Guid id);

    void Update(Product product);

    void Delete(Guid id);

    Product? DecreaseStock(Guid productId, int quantity);

    void IncreaseStock(Guid productId, int quantity);
}