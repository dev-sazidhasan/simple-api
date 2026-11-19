using SimpleNameApi.DTOs.Requests;
using SimpleNameApi.DTOs.Responses;

namespace SimpleNameApi.Interfaces;

public interface IOrderService
{
    OrderResponse Create(Guid userId, CreateOrderRequest request);

    List<OrderResponse> GetMyOrders(Guid userId);

    OrderResponse? GetById(Guid userId, Guid orderId);

    bool Cancel(Guid userId, Guid orderId);
}