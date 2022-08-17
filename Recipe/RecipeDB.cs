using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class RecipeDB
{
    public readonly IMongoCollection<Recipe> _recipeCollection;

    public RecipeDB(
        IOptions<RecipeDatabaseSettings> recipeDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            recipeDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            recipeDatabaseSettings.Value.DatabaseName);

        _recipeCollection = mongoDatabase.GetCollection<Recipe>(
            recipeDatabaseSettings.Value.RecipeCollectionName);
    }
}