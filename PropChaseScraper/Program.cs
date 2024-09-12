using PropChaseScraper.Models;
using PropChaseScraper.SiteHandlers;

DataBank db = new DataBank();
Handler c_handler = new CentrisScrapingHandler();
Handler rlp_handler = new RoyalLePageScrapingHandler();

c_handler.NextHandler = rlp_handler;

c_handler.Handle(db);
