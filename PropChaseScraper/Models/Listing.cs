using System.Collections;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Linq;

namespace PropChaseScraper.Models;

public class Listing
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    
    public byte? Score { get; set; }
    public string? Type { get; set; }
    public string? Site { get; set; }
    public double? Sqft { get; set; }
    public string? Address { get; set; }
    public string? Neighbourhood { get; set; }
    public string? Url { get; set; }
    public int? NumBedrooms { get; set; }
    public int? NumBathrooms { get; set; }
    public double? Price { get; set; }
    public double? NumRooms { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    
    public Listing(string type, string site, double sqft, string address, string url, int numBedrooms, int numBathrooms, double price, double numRooms)
    {
        Id = ObjectId.GenerateNewId();
        Type = type;
        Site = site;
        Sqft = sqft;
        Address = address;
        Url = url;
        NumBedrooms = numBedrooms;
        NumBathrooms = numBathrooms;
        Price = price;
        NumRooms = numRooms;
        FindCoordinatesAsync();
        FindNeighbourhoodAsync();
    }
    
    public override string ToString()
    {
        return $"Listing: {Id}\n" +
               $"Type: {Type}\n" +
               $"Site: {Site}\n" +
               $"Sqft: {Sqft}\n" +
               $"Address: {Address}\n" +
               $"URL: {Url}\n" +
               $"Bedrooms: {NumBedrooms}\n" +
               $"Bathrooms: {NumBathrooms}\n" +
               $"Price: {Price}\n" +
               $"Score: {Score}\n" +
               $"Neighbourhood: {Neighbourhood}\n" +
               $"Coordinates: {Latitude}, {Longitude}\n";
    }

    private async Task FindCoordinatesAsync()
    {
        if (!string.IsNullOrEmpty(Address))
        {
            string apiKey = "AIzaSyCSY7Zjmy0do5EzOjqecmYbymiLerdtaM0";
            string requestUri = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(Address)}&key={apiKey}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(content);
                    if (json["status"].ToString() == "OK")
                    {
                        var location = json["results"][0]["geometry"]["location"];
                        Latitude = (double)location["lat"];
                        Longitude = (double)location["lng"];
                    }
                    else
                    {
                        Console.WriteLine("Geocoding failed: " + json["status"]);
                    }
                }
            }
        }
    }
    private async Task FindNeighbourhoodAsync()
    {
        if (Latitude.HasValue && Longitude.HasValue)
        {
            string apiKey = "AIzaSyCSY7Zjmy0do5EzOjqecmYbymiLerdtaM0"; // Replace with your API key
            string requestUri = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={Latitude},{Longitude}&key={apiKey}";

            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(3); // Set timeout to 3 minutes
                HttpResponseMessage response = await client.GetAsync(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(content);
                    if (json["status"].ToString() == "OK")
                    {
                        // Parsing the neighborhood from the reverse geocoding result
                        var addressComponents = json["results"][0]["address_components"];
                        foreach (var component in addressComponents)
                        {
                            var types = component["types"].Select(t => t.ToString()).ToList();
                            if (types.Contains("neighborhood"))
                            {
                                Neighbourhood = component["long_name"].ToString();
                                break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Reverse geocoding failed: " + json["status"]);
                    }
                }
            }
        }
    }

}