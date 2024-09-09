package org.example.WebScraperHandler;

import org.example.ListingDataBanks.IDataBank;

public interface IHandler {
    void setNextHandler(IHandler handler);
    void handle(IDataBank dataBank);
}
