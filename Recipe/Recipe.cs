using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Recipe
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Name")]
    public string RecipeName { get; set; } = null!;

    public List<string> Ingredients { get; set; } = null!;

    public List<string> Instructions { get; set; } = null!;

    public Recipe(){}
}