using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlazorShop.Application.ReadModels;

public class CustomerReadModel
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;
}