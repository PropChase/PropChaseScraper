using System.Collections;
using PropChaseScraper.DataLayer;
using PropChaseScraper.Scrapers;

namespace PropChaseScraper.Models;

public class DataBank
{
    private static DataBank _instance = new DataBank();
    private List<Listing> Listings { get; set; }
    
    private DataBank()
    {
        Listings = new List<Listing>();
    }   

    private void EmptyListings()
    {
        Listings.Clear();
    }

    public void GetData(CentrisSiteScraper scraper)
    {
        Listings = scraper.GetListings();
        PostToDatabase();
    }
    
    public void GetData(RoyalLePageScraper scraper)
    {
        scraper.Scrape(this);
        PostToDatabase();
    }

    private void PostToDatabase()
    {
        MongoDBInterface db = MongoDBInterface.Instance;
        db.PostListings(Listings);
        EmptyListings();
    }
    
    public static DataBank Instance
    {
        get
        {
            return _instance;
        }
    }
}