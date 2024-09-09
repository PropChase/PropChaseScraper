package org.example.SiteScrapers;

import org.example.Listing.Listing;
import org.example.ListingDataBanks.IDataBank;
import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;

import java.io.IOException;
import java.util.ArrayList;

public class CentrisSiteScraper implements ISiteScraper{
    public ArrayList<Listing> scrapeCentrisSite() throws Exception {
        return cleanUpRawHTML(scrapeRawHTML());
    }
    private ArrayList<String> scrapeRawHTML() throws IOException {
        ArrayList<String> listings = new ArrayList<>();

        Document doc = Jsoup.connect("https://www.centris.ca/en/properties~for-sale~montreal-island?view=Thumbnail&utm_source=google&utm_medium=paid&utm_campaign=20547519783&utm_content=158952966872&utm_term=&gad_source=1&gbraid=0AAAAADy2M1GMrxbqlcqiswSf-yNkG0vnc&gclid=Cj0KCQjwtsy1BhD7ARIsAHOi4xY9lETR3qlsJy9yxux8iTjZMZhrYdPuMkHaPSERZuWqZ-FUwF57TNQaAoptEALw_wcB").get();
        Elements listingCards = doc.select("div.property-thumbnail-item");

        for (Element listingCard : listingCards) {
            listings.add(listingCard.html());
        }

        return listings;
    }
    private ArrayList<Listing> cleanUpRawHTML(ArrayList<String> rawListingsHTML) {
        ArrayList<Listing> listings = new ArrayList<>();

        for (String listingHTML : rawListingsHTML) {
            Listing listing = new Listing();
            Document doc = Jsoup.parse(listingHTML);

            listing.setSite("Centris");

            Element addressElement = doc.select(".location-container .address div").first();
            String address = addressElement.text();
            listing.setAddress(address);

            listings.add(listing);
        }

        return listings;
    }

    @Override
    public void fillDataBank(IDataBank dataBank) throws Exception {
        dataBank.getData(this);
    }
}
