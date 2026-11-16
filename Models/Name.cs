
namespace SimpleNameApi.Models;

public class Name
{
    public Guid Id { get; set; }

    public string NameValue { get; set; } = string.Empty;

    public DateTime CreatedAtUtc { get; set; }
}
