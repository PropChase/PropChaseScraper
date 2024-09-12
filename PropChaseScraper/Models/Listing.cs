using System.Collections;

namespace PropChaseScraper.Models;

public class Listing
{
    public string Id { get; set; }
    public string Type { get; set; }
    public string Site { get; set; }
    public ArrayList Images { get; set; }
    public double Sqft { get; set; }
    public string Address { get; set; }
    public string Url { get; set; }
    public int NumBedrooms { get; set; }
    public int NumBathrooms { get; set; }
    public double Price { get; set; }
    
    public Listing(string id, string type, string site, ArrayList images, double sqft, string address, string url, int numBedrooms, int numBathrooms, double price, string rawListing)
    {
        Id = id;
        Type = type;
        Site = site;
        Images = images;
        Sqft = sqft;
        Address = address;
        Url = url;
        NumBedrooms = numBedrooms;
        NumBathrooms = numBathrooms;
        Price = price;
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
               $"Price: {Price}\n";
    }
}