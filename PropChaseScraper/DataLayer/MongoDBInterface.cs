using MongoDB.Bson;
using MongoDB.Driver;
using PropChaseScraper.Models;

namespace PropChaseScraper.DataLayer;

public class MongoDBInterface
{
    private static MongoDBInterface instance = new MongoDBInterface();
    public static string connectionUri = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_URI");
    
    private MongoDBInterface() {}
    
    public void PostListings(List<Listing> listings)
    {
        var settings = MongoClientSettings.FromConnectionString(connectionUri);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        var client = new MongoClient(settings);
        var database = client.GetDatabase("Listings"); 

        var collection = database.GetCollection<Listing>("Listings");

        collection.InsertMany(listings);
    }
    
    public static MongoDBInterface Instance
    {
        get
        {
            return instance;
        }
    }
}