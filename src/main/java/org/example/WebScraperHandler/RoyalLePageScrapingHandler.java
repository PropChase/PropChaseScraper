package org.example.WebScraperHandler;

import org.example.DataLayer.MongoDB;
import org.example.ListingDataBanks.IDataBank;
import org.example.SiteScrapers.CentrisSiteScraper;
import org.example.SiteScrapers.ISiteScraper;
import org.example.SiteScrapers.RoyalLePageSiteScraper;

public class RoyalLePageScrapingHandler extends BaseHandler{
    @Override
    public void handle(IDataBank dataBank) {
        ISiteScraper siteScraper = new RoyalLePageSiteScraper();

        try {
            siteScraper.fillDataBank(dataBank);
            MongoDB.clearListings("RoyalLePage");
            MongoDB.getInstance().postListings(dataBank.getListings(), "RoyalLePage");
            dataBank.emptyListings();
        } catch (Exception e) {
            System.out.println("ERROR TEXT SENT");
            dataBank.emptyListings();
        }

        if (nextHandler != null) {
            nextHandler.handle(dataBank);
        }
    }
}
