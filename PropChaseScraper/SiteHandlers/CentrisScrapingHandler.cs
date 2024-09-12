using PropChaseScraper.Models;

namespace PropChaseScraper.SiteHandlers;

public class CentrisScrapingHandler : Handler
{
    public override void Handle(DataBank dataBank)
    {
        Console.WriteLine("Handle Centris site scraper");
        base.Handle(dataBank);
    }
}