package org.example.ListingDataBanks;

import org.example.Listing.Listing;
import org.example.SiteScrapers.CentrisSiteScraper;
import org.example.SiteScrapers.RoyalLePageSiteScraper;

import java.util.ArrayList;

public interface IDataBank {
    void getData(RoyalLePageSiteScraper royalLePageScraper) throws Exception;
    void getData(CentrisSiteScraper centrisScraper) throws Exception;
    void printListings();
    ArrayList<Listing> getListings();
    void emptyListings();
}
