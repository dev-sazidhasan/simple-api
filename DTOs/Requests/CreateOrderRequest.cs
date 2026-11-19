using System.ComponentModel.DataAnnotations;

namespace SimpleNameApi.DTOs.Requests;

public class CreateOrderRequest
{
    [Required(ErrorMessage = "Product ID is required.")]
    public Guid ProductId { get; set; }

    [Range(1, 1000,
        ErrorMessage = "Quantity must be between 1 and 1000.")]
    public int Quantity { get; set; }
}