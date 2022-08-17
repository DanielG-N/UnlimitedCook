using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class CookBook
{
    [BsonId]
    public int Id { get; set; }

    public List<string> RecipeIds { get; set; } = null!;
}