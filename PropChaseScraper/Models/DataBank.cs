using System.Collections;

namespace PropChaseScraper.Models;

public class DataBank
{
    public ArrayList Listings { get; set; }
    
    public DataBank()
    {
        Listings = new ArrayList();
    }   

    public void EmptyListings()
    {
        Listings.Clear();
    }

    public void getData()
    {
        
    }
}