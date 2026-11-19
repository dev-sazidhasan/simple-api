using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SimpleNameApi.Models;

public class User
{
    [BsonId]
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; }

    [BsonElement("fullName")]
    public string FullName { get; set; } = string.Empty;

    [BsonElement("email")]
    public string Email { get; set; } = string.Empty;

    [BsonElement("passwordHash")]
    public string PasswordHash { get; set; } = string.Empty;

    [BsonElement("role")]
    public string Role { get; set; } = "Customer";

    [BsonElement("createdAtUtc")]
    public DateTime CreatedAtUtc { get; set; }
}