package org.example.WebScraperHandler;

import org.example.ListingDataBanks.IDataBank;

public class BaseHandler implements IHandler{
    IHandler nextHandler;

    @Override
    public void setNextHandler(IHandler handler) {
        this.nextHandler = handler;
    }

    @Override
    public void handle(IDataBank dataBank) {
        if (this.nextHandler != null) {
            this.nextHandler.handle(dataBank);
        }
    }
}
