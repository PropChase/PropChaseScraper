using MongoDB.Bson;
using MongoDB.Driver;
using PropChaseScraper.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        
        ScoreListing(listings);

        collection.InsertMany(listings);
    }
    
    private void ScoreListing(List<Listing> listings)
    {
        // categorize by neighbourhood
        List<List<Listing>> listingsByNeighbourhood = new List<List<Listing>>();
        
        foreach (var listing in listings)
        {
            bool found = false;
            foreach (var neighbourhoodListings in listingsByNeighbourhood)
            {
                if (neighbourhoodListings[0].Neighbourhood == listing.Neighbourhood)
                {
                    neighbourhoodListings.Add(listing);
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                listingsByNeighbourhood.Add(new List<Listing> {listing});
            }
        }
            
        foreach (var neighbourhoodListings in listingsByNeighbourhood)
        {
            double interval = 100.0 / neighbourhoodListings.Count;
            double score = 100;
                
            List<Listing> sortedListings = neighbourhoodListings.OrderBy(listing => listing.Price / listing.Sqft).ToList();
    
            foreach (var listing in sortedListings)
            {
                listing.Score = Convert.ToByte(Math.Round(score));
                score -= interval;
                    
                if (score < 0)
                {
                    score = 0;
                }
            }
        }
    }
    
    public static MongoDBInterface Instance
    {
        get
        {
            return instance;
        }
    }
}