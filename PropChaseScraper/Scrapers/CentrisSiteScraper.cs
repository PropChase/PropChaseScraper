using System.Text;
using HtmlAgilityPack;
using PropChaseScraper.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text.RegularExpressions;

namespace PropChaseScraper.Scrapers;

public class CentrisSiteScraper : ISiteScraper
{ 
    private List<string> ScrapePropertyLinks()
    {
        Console.WriteLine("Scraping Centris...");
        var url = "https://www.centris.ca/en/properties~for-sale~montreal-island?view=Thumbnail&utm_source=google&utm_medium=paid&utm_campaign=20547519783&utm_content=158952966872&utm_term=&gad_source=1&gbraid=0AAAAADy2M1GMrxbqlcqiswSf-yNkG0vnc&gclid=Cj0KCQjwtsy1BhD7ARIsAHOi4xY9lETR3qlsJy9yxux8iTjZMZhrYdPuMkHaPSERZuWqZ-FUwF57TNQaAoptEALw_wcB&uc=0";
        var links = new List<string>();

        using (var driver = new ChromeDriver())
        {
            driver.Navigate().GoToUrl(url);

            // Dismiss the popup
            try
            {
                var agreeButton = driver.FindElement(By.Id("didomi-notice-agree-button"));
                agreeButton.Click();
            }
            catch (NoSuchElementException)
            {
                // Handle the case where the agree button is not found.
            }

            while (true)
            {
                var propertyDivs = driver.FindElements(By.XPath("//div[contains(@class, 'property-thumbnail-item')]"));

                for (int i = 0; i < propertyDivs.Count; i++)
                {
                    try
                    {
                        var div = driver.FindElement(By.XPath("(//div[contains(@class, 'property-thumbnail-item')])[" + (i + 1) + "]"));

                        // Find the link node within the div
                        var linkNode = div.FindElement(By.XPath(".//a[contains(@class, 'property-thumbnail-summary-link')]"));
                        if (linkNode != null)
                        {
                            var link = linkNode.GetAttribute("href");
                            if (!string.IsNullOrEmpty(link))
                            {
                                links.Add(link);
                            }
                        }
                    }
                    catch (StaleElementReferenceException)
                    {
                        Console.WriteLine(
                            "Element is no longer attached to the DOM. Continue with the next iteration.");
                        continue;
                    }
                }

                var pagerCurrent = driver.FindElement(By.XPath("//li[contains(@class, 'pager-current')]"));
                if (pagerCurrent != null)
                {
                    var pagerText = pagerCurrent.Text; // "1 / 417 +"
                    var pages = pagerText.Split('/');
                    if (pages.Length == 2)
                    {
                        var currentPage = int.Parse(pages[0].Trim());
                        var totalPages = int.Parse(Regex.Replace(pages[1], @"[^\d]", "").Trim());

                        if (currentPage == totalPages)
                        {
                            break;
                        }
                    }
                }

                try
                {
                    var nextButton = driver.FindElement(By.XPath("//li[contains(@class, 'next')]/a"));
                    nextButton.Click();
                    Thread.Sleep(500);
                }
                catch (NoSuchElementException)
                {
                    break; 
                }
            }
        }
        Console.WriteLine("Centris scraped.\n \n \n");
        return links;
    }
    private List<Listing> MapLinksToListings(List<string> links)
    {
        Console.WriteLine("Mapping Centris links to listings...");
        List<Listing> listings = new List<Listing>();
        var web = new HtmlWeb();
        
        int currentLink = 0;
        
        foreach (string link in links)
        {
            Console.WriteLine($"Mapping link {currentLink++} of {links.Count}...");
            // Load the page
            var doc = web.Load(link);
            
            // fetch pertinent sections of the page by their XPath
            var priceNode = doc.DocumentNode.SelectSingleNode("//span[@id='BuyPrice']");
            var typeNode = doc.DocumentNode.SelectSingleNode("//span[@data-id='PageTitle']");
            var sqftNode = doc.DocumentNode.SelectSingleNode("//div[@class='carac-value']/span[contains(text(), 'sqft')]");
            var address = doc.DocumentNode.SelectSingleNode("//h2[@itemprop='address']");
            var bedroomNode = doc.DocumentNode.SelectSingleNode("//div[@class='col-lg-3 col-sm-6 cac']");
            var bathroomNode = doc.DocumentNode.SelectSingleNode("//div[@class='col-lg-3 col-sm-6 sdb']");
            var roomNode = doc.DocumentNode.SelectSingleNode("//div[@class='col-lg-3 col-sm-6 piece']");

            if (priceNode != null && typeNode != null && sqftNode != null && bathroomNode != null && bedroomNode != null && address != null && roomNode != null)
            {
                Listing listing = new Listing(
                    ExtractType(typeNode), 
                    "Centris", 
                    ExtractSqft(sqftNode),
                    ExtractAddress(address), 
                    link, 
                    ExtractBedrooms(bedroomNode), 
                    ExtractBathrooms(bathroomNode),
                    ExtractPrice(priceNode), 
                    ExtractRooms(roomNode));
                
                listings.Add(listing);
            }
        }
        Console.WriteLine("Centris links mapped to listings.\n \n \n");
        return listings;
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
        var numericString = new StringBuilder();

        foreach (var ch in bathroomString)
        {
            if (char.IsDigit(ch))
            {
                numericString.Append(ch);
            }
            else
            {
                break;
            }
        }

        if (numericString.ToString() == "")
        {
            return 0;
        }
        else
        {
            return int.Parse(numericString.ToString());
        }
    }
    private int ExtractBedrooms(HtmlNode bedroomNode)
    {
        var bedroomString = bedroomNode.InnerHtml;
        var numericString = new StringBuilder();

        foreach (var ch in bedroomString)
        {
            if (char.IsDigit(ch))
            {
                numericString.Append(ch);
            }
            else
            {
                break;
            }
        }

        if (numericString.ToString() == "")
        {
            return 0;
        }
        else
        {
            return int.Parse(numericString.ToString());
        }
    }
    private double ExtractRooms(HtmlNode roomNode)
    {
        var roomString = roomNode.InnerHtml;
        roomString = roomString.Replace("room", "").Trim();
        
        if (roomString.Contains("s"))
        {
            roomString = roomString.Replace("s", "").Trim();
        }
        
        return double.Parse(roomString);
    }
    private string ExtractAddress(HtmlNode addressNode)
    {
        return addressNode.InnerHtml.Trim();
    }
    
    public List<Listing> GetListings()
    {
        List<string> links = ScrapePropertyLinks();
        return MapLinksToListings(links);
    }
    
    public void Scrape()
    {
        DataBank db = DataBank.Instance;
        db.GetData(this);
    }
}