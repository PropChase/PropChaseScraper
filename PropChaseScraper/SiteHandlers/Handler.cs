using PropChaseScraper.Models;

namespace PropChaseScraper.SiteHandlers;

public abstract class Handler
{
    public Handler? NextHandler { get; set; }
    
    public virtual void Handle(DataBank dataBank)
    {
        if (NextHandler != null)
        {
            NextHandler.Handle(dataBank);
        }
    }
}