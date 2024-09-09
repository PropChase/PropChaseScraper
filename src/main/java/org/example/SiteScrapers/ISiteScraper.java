package org.example.SiteScrapers;

import org.example.ListingDataBanks.IDataBank;

public interface ISiteScraper {
    void fillDataBank(IDataBank dataBank) throws Exception;
}
