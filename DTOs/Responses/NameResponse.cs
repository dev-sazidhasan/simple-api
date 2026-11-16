
namespace SimpleNameApi.DTOs.Responses;

public class NameResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateTime CreatedAtUtc { get; set; }
}
