using HtmlAgilityPack;
using PropChaseScraper.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PropChaseScraper.Scrapers;

public class CentrisSiteScraper : ISiteScraper
{ 
    private List<string> GetAllLinksToListings()
    {
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
                    // Re-find the div on each iteration to avoid StaleElementReferenceException
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

        return links;
    }

    public void MapLinksToListings(List<string> links)
    {
        var listings = new List<Listing>();
        var web = new HtmlWeb();
        
        foreach (string link in links)
        {
            // Load the page
            var doc = web.Load(link);
            
            // fetch pertinent sections of the page
            var priceNode = doc.DocumentNode.SelectSingleNode("//span[@id='BuyPrice']");
            var TypeNode = doc.DocumentNode.SelectSingleNode("//span[@data-id='PageTitle']");
            var netAreaNode = doc.DocumentNode.SelectSingleNode("//div[.//div[text()='Net area']]");

            if (priceNode != null)
            {
                // Get house price
                var priceString = priceNode.InnerHtml;
                priceString = priceString.Replace("$", "").Replace(",", "");
                double price = double.Parse(priceString);
                
                // Get house type
                var typeString = TypeNode.InnerHtml;
                
                // Set house site
                var site = "Centris";
                
                // Get sqft
                var parentDiv = netAreaNode.ParentNode;
                var sqftNode = parentDiv.SelectSingleNode("following-sibling::div");
                Console.WriteLine(sqftNode.InnerHtml);
                double sqft = 0;
                
                if (sqftNode != null)
                {
                    // Get the square footage as a string
                    var sqftString = sqftNode.InnerHtml;

                    // Remove the "sqft" from the string
                    sqftString = sqftString.Replace("sqft", "").Trim();

                    // Parse the string to a double
                    sqft = double.Parse(sqftString);
                }

                Console.WriteLine($"Price: {price} \n" +
                                  $"Type: {typeString} \n" +
                                  $"Site: {site} \n" +
                                  $"Sqft: {sqft} \n" +
                                  $"" );
            }
            
        }
    }
    
    public void FillDataBank(DataBank dataBank)
    {
        List<string> links = GetAllLinksToListings();
        
        Console.WriteLine("Fill data bank with Centris data");
    }
}