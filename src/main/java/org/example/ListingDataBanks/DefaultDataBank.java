package org.example.ListingDataBanks;

import org.example.Listing.Listing;
import org.example.SiteScrapers.CentrisSiteScraper;
import org.example.SiteScrapers.RoyalLePageSiteScraper;

import java.util.ArrayList;

public class DefaultDataBank implements IDataBank {
    private ArrayList<Listing> listings = new ArrayList<Listing>();

    @Override
    public void getData(RoyalLePageSiteScraper royalLePageScraper) throws Exception {
        listings.addAll(royalLePageScraper.scrapeRoyalLePageSite());
    }

    @Override
    public void getData(CentrisSiteScraper centrisScraper) throws Exception {
        listings.addAll(centrisScraper.scrapeCentrisSite());
    }

    @Override
    public void emptyListings() {
        listings.clear();
    }

    @Override
    public void printListings() {
        for (Listing listing : listings) {
            System.out.println(listing);
        }
    }

    @Override
    public ArrayList<Listing> getListings() {
        return listings;
    }
}
