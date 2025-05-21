namespace api.models;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Link
{
	[BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
	public string? Id { get; set; }

	[BsonElement("code")]
	public required string Code { get; set; }

	[BsonElement("url")]
	public required string Url { get; set; }

	[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
	[BsonElement("createdAt")]
	public DateTime CreatedAt { get; set; } = DateTime.Now;
}
