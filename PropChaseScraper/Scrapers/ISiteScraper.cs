using PropChaseScraper.Models;

namespace PropChaseScraper.Scrapers;

public interface ISiteScraper
{
    void FillDataBank(DataBank dataBank);
}