using SimpleNameApi.DTOs.Requests;
using SimpleNameApi.DTOs.Responses;
using SimpleNameApi.Interfaces;
using SimpleNameApi.Models;

namespace SimpleNameApi.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public ProductResponse Create(CreateProductRequest request)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            Description = request.Description.Trim(),
            Price = request.Price,
            Stock = request.Stock,
            Category = request.Category.Trim(),
            CreatedAtUtc = DateTime.UtcNow
        };

        _productRepository.Add(product);

        return MapToResponse(product);
    }

    public List<ProductResponse> GetAll()
    {
        var products = _productRepository.GetAll();

        return products.Select(MapToResponse).ToList();
    }

    public ProductResponse? GetById(Guid id)
    {
        var product = _productRepository.GetById(id);

        if (product is null)
        {
            return null;
        }

        return MapToResponse(product);
    }

    public ProductResponse? Update(
        Guid id,
        UpdateProductRequest request)
    {
        var product = _productRepository.GetById(id);

        if (product is null)
        {
            return null;
        }

        product.Name = request.Name.Trim();
        product.Description = request.Description.Trim();
        product.Price = request.Price;
        product.Stock = request.Stock;
        product.Category = request.Category.Trim();

        _productRepository.Update(product);

        return MapToResponse(product);
    }

    public bool Delete(Guid id)
    {
        var product = _productRepository.GetById(id);

        if (product is null)
        {
            return false;
        }

        _productRepository.Delete(id);

        return true;
    }

    private static ProductResponse MapToResponse(Product product)
    {
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            Category = product.Category,
            CreatedAtUtc = product.CreatedAtUtc
        };
    }
}