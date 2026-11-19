using MongoDB.Driver;
using SimpleNameApi.Data;
using SimpleNameApi.Interfaces;
using SimpleNameApi.Models;

namespace SimpleNameApi.Repositories;

public class MongoOrderRepository : IOrderRepository
{
    private readonly MongoDbContext _context;

    public MongoOrderRepository(MongoDbContext context)
    {
        _context = context;
    }

    public void Add(Order order)
    {
        _context.Orders.InsertOne(order);
    }

    public List<Order> GetByUserId(Guid userId)
    {
        return _context.Orders
            .Find(order => order.UserId == userId)
            .SortByDescending(order => order.CreatedAtUtc)
            .ToList();
    }

    public Order? GetById(Guid id)
    {
        return _context.Orders
            .Find(order => order.Id == id)
            .FirstOrDefault();
    }

    public void Update(Order order)
    {
        _context.Orders.ReplaceOne(
            existingOrder => existingOrder.Id == order.Id,
            order
        );
    }
    public List<Order> GetAll()
{
    return _context.Orders
        .Find(_ => true)
        .SortByDescending(order => order.CreatedAtUtc)
        .ToList();
}
}