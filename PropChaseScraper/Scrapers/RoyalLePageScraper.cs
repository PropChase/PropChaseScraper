using HtmlAgilityPack;
using PropChaseScraper.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text.RegularExpressions;

namespace PropChaseScraper.Scrapers;

public class RoyalLePageScraper
{
    private List<string> ScrapePropertyLinks()
    {
        var url = "https://www.centris.ca/en/properties~for-sale~montreal-island?view=Thumbnail&utm_source=google&utm_medium=paid&utm_campaign=20547519783&utm_content=158952966872&utm_term=&gad_source=1&gbraid=0AAAAADy2M1GMrxbqlcqiswSf-yNkG0vnc&gclid=Cj0KCQjwtsy1BhD7ARIsAHOi4xY9lETR3qlsJy9yxux8iTjZMZhrYdPuMkHaPSERZuWqZ-FUwF57TNQaAoptEALw_wcB&uc=0";
        var links = new List<string>();
        
        return links;
    }
    private void MapLinksToListings(List<string> links)
    {
        
    }
    private double ExtractPrice(HtmlNode priceNode)
    {
        var priceString = priceNode.InnerHtml;
        priceString = priceString.Replace("$", "").Replace(",", "");
        return double.Parse(priceString);
    }
    private string ExtractType(HtmlNode typeNode)
    {
        return typeNode.InnerHtml;
    }
    private double ExtractSqft(HtmlNode sqftNode)
    {
        var sqftString = sqftNode.InnerHtml;
        sqftString = sqftString.Replace("sqft", "").Trim();
        return double.Parse(sqftString);
    }
    private int ExtractBathrooms(HtmlNode bathroomNode)
    {
        var bathroomString = bathroomNode.InnerHtml;
        bathroomString = bathroomString.Replace("bathroom", "").Trim();
        return int.Parse(bathroomString);
    }
    private int ExtractBedrooms(HtmlNode bedroomNode)
    {
        var bedroomString = bedroomNode.InnerHtml;
        bedroomString = bedroomString.Replace("bedroom", "").Trim();
        return int.Parse(bedroomString);
    }
    private double ExtractRooms(HtmlNode roomNode)
    {
        var roomString = roomNode.InnerHtml;
        roomString = roomString.Replace("rooms", "").Trim();
        return double.Parse(roomString);
    }
    private string ExtractAddress(HtmlNode addressNode)
    {
        return addressNode.InnerHtml.Trim();
    }
    
    public void Scrape(DataBank dataBank)
    {
        List<string> links = ScrapePropertyLinks();
        MapLinksToListings(links);
    }
}