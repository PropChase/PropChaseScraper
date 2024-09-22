using MongoDB.Bson;
using MongoDB.Driver;
using PropChaseScraper.Models;
using PropChaseScraper.SiteHandlers;

namespace PropChaseScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            Handler centrisHandler = new CentrisScrapingHandler();
            centrisHandler.Handle();
        }
    }
}

