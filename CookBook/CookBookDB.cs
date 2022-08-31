using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class CookBookDB
{
    public IMongoCollection<CookBook> _cookBookCollection;

    public CookBookDB(){}
    public CookBookDB(
        IOptions<CookBookDatabaseSettings> cookBookDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            cookBookDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            cookBookDatabaseSettings.Value.DatabaseName);

        _cookBookCollection = mongoDatabase.GetCollection<CookBook>(
            cookBookDatabaseSettings.Value.CookBookCollectionName);
    }
}