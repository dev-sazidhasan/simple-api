using SimpleNameApi.DTOs.Requests;
using SimpleNameApi.DTOs.Responses;
using SimpleNameApi.Interfaces;
using SimpleNameApi.Models;

namespace SimpleNameApi.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public OrderService(
        IOrderRepository orderRepository,
        IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public OrderResponse Create(Guid userId, CreateOrderRequest request)
    {
        var product = _productRepository.GetById(request.ProductId);

        if (product == null)
        {
            throw new InvalidOperationException("Product not found.");
        }

        var updatedProduct = _productRepository.DecreaseStock(
            request.ProductId,
            request.Quantity
        );

        if (updatedProduct == null)
        {
            throw new InvalidOperationException(
                "Insufficient product stock."
            );
        }

        var order = new Order
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            ProductId = updatedProduct.Id,
            ProductName = updatedProduct.Name,
            UnitPrice = updatedProduct.Price,
            Quantity = request.Quantity,
            TotalAmount = updatedProduct.Price * request.Quantity,
            Status = "Pending",
            CreatedAtUtc = DateTime.UtcNow
        };

        try
        {
            _orderRepository.Add(order);
        }
        catch
        {
            _productRepository.IncreaseStock(
                request.ProductId,
                request.Quantity
            );

            throw;
        }

        return MapToResponse(order);
    }

    public List<OrderResponse> GetMyOrders(Guid userId)
    {
        var orders = _orderRepository.GetByUserId(userId);

        return orders.Select(MapToResponse).ToList();
    }

    public OrderResponse? GetById(Guid userId, Guid orderId)
    {
        var order = _orderRepository.GetById(orderId);

        if (order == null || order.UserId != userId)
        {
            return null;
        }

        return MapToResponse(order);
    }

    public bool Cancel(Guid userId, Guid orderId)
    {
        var order = _orderRepository.GetById(orderId);

        if (order == null || order.UserId != userId)
        {
            return false;
        }

        if (order.Status != "Pending")
        {
            return false;
        }

        order.Status = "Cancelled";

        _orderRepository.Update(order);

        _productRepository.IncreaseStock(
            order.ProductId,
            order.Quantity
        );

        return true;
    }

    private static OrderResponse MapToResponse(Order order)
    {
        return new OrderResponse
        {
            Id = order.Id,
            UserId = order.UserId,
            ProductId = order.ProductId,
            ProductName = order.ProductName,
            UnitPrice = order.UnitPrice,
            Quantity = order.Quantity,
            TotalAmount = order.TotalAmount,
            Status = order.Status,
            CreatedAtUtc = order.CreatedAtUtc
        };
    }
}