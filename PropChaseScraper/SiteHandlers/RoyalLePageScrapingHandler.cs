using PropChaseScraper.Models;

namespace PropChaseScraper.SiteHandlers;

public class RoyalLePageScrapingHandler : Handler
{
    public override void Handle(DataBank dataBank)
    {
        Console.WriteLine("Handle Royal LePage site scraper");
        base.Handle(dataBank);
    }
}