using MongoDB.Bson;
using MongoDB.Driver;
using PropChaseScraper.Models;
using Newtonsoft.Json;

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
    
    private void ScoreListings(List<Listing> listings)
    {
        Dictionary<string, List<Listing>> neighborhoodGroups = new Dictionary<string, List<Listing>>();

        foreach (var listing in listings)
        {
            string neighborhood = ExtractNeighborhoodFromAddress(listing.Address); // You need to implement this method

            if (neighborhoodGroups.ContainsKey(neighborhood))
            {
                neighborhoodGroups[neighborhood].Add(listing);
            }
            else
            {
                neighborhoodGroups[neighborhood] = new List<Listing> { listing };
            }
        }
    }
    
    //AIzaSyAVDexlW2Oi7N1nIRtxG6sOeIPamrIJCHo
    private string ExtractNeighborhoodFromAddress(string address)
    {
        string googleMapsApiKey = Environment.GetEnvironmentVariable("AIzaSyAVDexlW2Oi7N1nIRtxG6sOeIPamrIJCHo");
        string requestUrl = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)}&key={googleMapsApiKey}";

        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = client.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dynamic json = JsonConvert.DeserializeObject(content);

                foreach (var result in json.results)
                {
                    foreach (var component in result.address_components)
                    {
                        foreach (var type in component.types)
                        {
                            if (type == "neighborhood")
                            {
                                return component.long_name;
                            }
                        }
                    }
                }
            }
        }
        return "Neighborhood";
    }
    
    public static MongoDBInterface Instance
    {
        get
        {
            return instance;
        }
    }
}