
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SimpleNameApi.Models;

public class Name
{
    [BsonId]
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; }

    [BsonElement("name")]
    public string NameValue { get; set; } = string.Empty;

    [BsonElement("createdAtUtc")]
    public DateTime CreatedAtUtc { get; set; }
}
