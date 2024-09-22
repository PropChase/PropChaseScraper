# PropChaseScraper

PropChaseScraper is a C# application designed to scrape property listings from various real estate websites and store them in a MongoDB database. The application categorizes and scores the listings based on their neighbourhood and price per square foot.

## Features

- Scrapes property listings from multiple real estate websites.
- Cerialize the data.
- Categorizes listings by neighbourhood.
- Scores listings based on price per square foot.
- Stores listings in a MongoDB database.

## Classes

- `Program`: The main entry point of the application
- `Listing`: Represents a property listing with various attributes such as type, site, square footage, address, URL, number of bedrooms, bathrooms, price, and score.
- `MongoDBInterface`: Handles the connection to the MongoDB database and provides methods for posting listings to the database.
- `DataBank`: Acts as a data store for the scraped listings and provides methods for getting data from the scrapers and posting it to the database.
- `ConcreteScraper' : These classes are the only ones using the databank. They + the databank form a strategy pattern in which the scrapers are resonsible for the logic used to scrape data from each site and the databank is the object holding the fields being changed by these operations.
- `ConcreteScraperHandlers' : These classes are a chain of responsability and are meant to be linked to each other when used. They dictate the applications flow, going siteScraper by siteScraper to scrape the data and insert it into the database while handling errors for each individual site differently.


## Usage

To run the application, simply execute the `Program.cs` file. The application will start scraping data from the specified real estate websites and store the listings in the MongoDB database.

## Dependencies

- MongoDB.Driver: For connecting to and interacting with MongoDB.
- Newtonsoft.Json: For handling JSON data.
- Agility: to Parse HTML content from sites

## Environment Variables

- `MONGODB_CONNECTION_URI`: The connection string for the MongoDB database.

## Note

This application is a work in progress and is subject to changes. Always pull the latest version before running the application.
