using System.Collections;

namespace PropChaseScraper.Models;

public class DataBank
{
    public List<Listing> Listings { get; set; }
    
    public DataBank()
    {
        Listings = new List<Listing>();
    }   

    public void EmptyListings()
    {
        Listings.Clear();
    }

    public void getData()
    {
        
    }
}