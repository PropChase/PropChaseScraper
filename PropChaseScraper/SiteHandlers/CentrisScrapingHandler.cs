using PropChaseScraper.Models;
using PropChaseScraper.Scrapers;

namespace PropChaseScraper.SiteHandlers;

public class CentrisScrapingHandler : Handler
{
    public override void Handle()
    {
        Console.WriteLine("Handle Centris site scraper");
        CentrisSiteScraper cs = new CentrisSiteScraper();
        
        try
        {
            cs.Scrape();
        }
        catch (Exception e)
        {
            this.Handle();
        }
        
        base.Handle();
    }
}