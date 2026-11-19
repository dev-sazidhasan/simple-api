using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleNameApi.DTOs.Requests;
using SimpleNameApi.Interfaces;
using System.Security.Claims;

namespace SimpleNameApi.Controllers;

[ApiController]
[Route("api/orders")]
[Authorize(Roles = "Customer")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public IActionResult Create(CreateOrderRequest request)
    {
        var userId = GetCurrentUserId();

        var order = _orderService.Create(userId, request);

        return CreatedAtAction(
            nameof(GetById),
            new { orderId = order.Id },
            order
        );
    }

    [HttpGet]
    public IActionResult GetMyOrders()
    {
        var userId = GetCurrentUserId();

        var orders = _orderService.GetMyOrders(userId);

        return Ok(orders);
    }

    [HttpGet("{orderId:guid}")]
    public IActionResult GetById(Guid orderId)
    {
        var userId = GetCurrentUserId();

        var order = _orderService.GetById(userId, orderId);

        if (order == null)
        {
            return NotFound(new
            {
                message = "Order not found."
            });
        }

        return Ok(order);
    }

    [HttpDelete("{orderId:guid}")]
    public IActionResult Cancel(Guid orderId)
    {
        var userId = GetCurrentUserId();

        var cancelled = _orderService.Cancel(userId, orderId);

        if (!cancelled)
        {
            return BadRequest(new
            {
                message = "Order cannot be cancelled."
            });
        }

        return Ok(new
        {
            message = "Order cancelled successfully."
        });
    }

    private Guid GetCurrentUserId()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userId, out var parsedUserId))
        {
            throw new UnauthorizedAccessException(
                "Invalid user authentication."
            );
        }

        return parsedUserId;
    }
}