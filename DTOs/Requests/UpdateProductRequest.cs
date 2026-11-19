using System.ComponentModel.DataAnnotations;

namespace SimpleNameApi.DTOs.Requests;

public class UpdateProductRequest
{
    [Required(ErrorMessage = "Product name is required.")]
    [StringLength(150, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Product description is required.")]
    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "999999999",
        ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue,
        ErrorMessage = "Stock cannot be negative.")]
    public int Stock { get; set; }

    [Required(ErrorMessage = "Product category is required.")]
    [StringLength(100)]
    public string Category { get; set; } = string.Empty;
}