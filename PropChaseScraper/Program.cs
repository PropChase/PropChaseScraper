using PropChaseScraper.Models;
using PropChaseScraper.Scrapers;
using PropChaseScraper.SiteHandlers;

// DataBank db = new DataBank();
// Handler cHandler = new CentrisScrapingHandler();
// Handler rlpHandler = new RoyalLePageScrapingHandler();
//
// cHandler.NextHandler = rlpHandler;
// cHandler.Handle(db);

CentrisSiteScraper scraper = new CentrisSiteScraper();
List<string> links = new List<string>();

links.Add("https://www.centris.ca/en/condos~for-sale~montreal-ville-marie/17020574?view=Summary&uc=5");

scraper.MapLinksToListings(links);