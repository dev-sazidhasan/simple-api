using SimpleNameApi.Models;

namespace SimpleNameApi.Interfaces;

public interface IOrderRepository
{
    void Add(Order order);

    List<Order> GetAll();

    List<Order> GetByUserId(Guid userId);

    Order? GetById(Guid id);

    void Update(Order order);
}