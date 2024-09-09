package org.example;
import org.example.ListingDataBanks.DefaultDataBank;
import org.example.WebScraperHandler.CentrisScrapingHandler;
import org.example.WebScraperHandler.IHandler;
import org.example.WebScraperHandler.RoyalLePageScrapingHandler;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

public class Main {
    public static void main(String[] args) {
        ScheduledExecutorService executor = Executors.newSingleThreadScheduledExecutor();

        Runnable task = () -> {
            DefaultDataBank dataBank = new DefaultDataBank();
            IHandler centrisHandler = new CentrisScrapingHandler();
            IHandler royalLePageHandler = new RoyalLePageScrapingHandler();

            centrisHandler.setNextHandler(royalLePageHandler);
            centrisHandler.handle(dataBank);
        };

        executor.scheduleAtFixedRate(task, 0, 5, TimeUnit.MINUTES);
    }
}