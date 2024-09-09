package org.example.SiteScrapers;

import org.example.Listing.Listing;
import org.example.ListingDataBanks.IDataBank;

import java.util.ArrayList;

public class RoyalLePageSiteScraper implements ISiteScraper{
    public ArrayList<Listing> scrapeRoyalLePageSite() throws Exception {
        ArrayList<Listing> listings = new ArrayList<Listing>();

        Listing listing = new Listing();
        listing.setAddress("123 Main St");
        listing.setSite("Royal LePage");
        listings.add(listing);

        return listings;
    }

    @Override
    public void fillDataBank(IDataBank dataBank) throws Exception {
        dataBank.getData(this);
    }
}
