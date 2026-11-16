
using System.ComponentModel.DataAnnotations;

namespace SimpleNameApi.DTOs.Requests;

public class CreateNameRequest
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(50, MinimumLength = 2,
        ErrorMessage = "Name must be between 2 and 50 characters.")]
    public string Name { get; set; } = string.Empty;
}
