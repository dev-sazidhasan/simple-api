using MongoDB.Driver;
using SimpleNameApi.Data;
using SimpleNameApi.Interfaces;
using SimpleNameApi.Models;

namespace SimpleNameApi.Repositories;

public class MongoProductRepository : IProductRepository
{
    private readonly MongoDbContext _context;

    public MongoProductRepository(MongoDbContext context)
    {
        _context = context;
    }

    public void Add(Product product)
    {
        _context.Products.InsertOne(product);
    }

    public List<Product> GetAll()
    {
        return _context.Products
            .Find(_ => true)
            .ToList();
    }

    public Product? GetById(Guid id)
    {
        return _context.Products
            .Find(product => product.Id == id)
            .FirstOrDefault();
    }

    public void Update(Product product)
    {
        _context.Products.ReplaceOne(
            existingProduct => existingProduct.Id == product.Id,
            product
        );
    }

    public void Delete(Guid id)
    {
        _context.Products.DeleteOne(
            product => product.Id == id
        );
    }
    public Product? DecreaseStock(Guid productId, int quantity)
{
    var filter = Builders<Product>.Filter.And(
        Builders<Product>.Filter.Eq(product => product.Id, productId),
        Builders<Product>.Filter.Gte(product => product.Stock, quantity)
    );

    var update = Builders<Product>.Update
        .Inc(product => product.Stock, -quantity);

    return _context.Products.FindOneAndUpdate(
        filter,
        update,
        new FindOneAndUpdateOptions<Product>
        {
            ReturnDocument = ReturnDocument.After
        }
    );
}

public void IncreaseStock(Guid productId, int quantity)
{
    var filter = Builders<Product>.Filter.Eq(
        product => product.Id,
        productId
    );

    var update = Builders<Product>.Update
        .Inc(product => product.Stock, quantity);

    _context.Products.UpdateOne(filter, update);
}
}