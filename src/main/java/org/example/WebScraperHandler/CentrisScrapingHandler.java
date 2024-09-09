package org.example.WebScraperHandler;

import org.example.DataLayer.MongoDB;
import org.example.ListingDataBanks.IDataBank;
import org.example.SiteScrapers.CentrisSiteScraper;
import org.example.SiteScrapers.ISiteScraper;

public class CentrisScrapingHandler extends BaseHandler{
    @Override
    public void handle(IDataBank dataBank) {
        ISiteScraper siteScraper = new CentrisSiteScraper();

        try {
            siteScraper.fillDataBank(dataBank);
            MongoDB.clearListings("Centris");
            MongoDB.postListings(dataBank.getListings(), "Centris");
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
