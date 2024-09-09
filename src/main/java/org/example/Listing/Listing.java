package org.example.Listing;

public class Listing {

    String id;
    String type;
    String site;
    Double sqft;
    String address;
    String url;
    Integer numBedrooms;
    Integer numBathrooms;
    Double price;
    String rawListing;

    public Listing(){}

    void setId(String id) { this.id = id; }
    void setType(String type) { this.type = type; }
    public void setSite(String site) { this.site = site; }
    void setSqft(Double sqft) { this.sqft = sqft; }
    public void setAddress(String address) { this.address = address; }
    void setUrl(String url) { this.url = url; }
    void setNumBedrooms(Integer numBedrooms) { this.numBedrooms = numBedrooms; }
    void setNumBathrooms(Integer numBathrooms) { this.numBathrooms = numBathrooms; }
    void setPrice(Double price) { this.price = price; }
    void setRawListing(String rawListing) { this.rawListing = rawListing; }

    String getId() { return this.id; }
    String getType() { return this.type; }
    public String getSite() { return this.site; }
    Double getSqft() { return this.sqft; }
    public String getAddress() { return this.address; }
    String getUrl() { return this.url; }
    Integer getNumBedrooms() { return this.numBedrooms; }
    Integer getNumBathrooms() { return this.numBathrooms; }
    Double getPrice() { return this.price; }
    String getRawListing() { return this.rawListing; }

    @Override
    public String toString() {
        return "Listing: " + getId() + "\n" +
                "Type: " + getType() + "\n" +
                "Site: " + getSite() + "\n" +
                "Sqft: " + getSqft() + "\n" +
                "Address: " + getAddress() + "\n" +
                "URL: " + getUrl() + "\n" +
                "Bedrooms: " + getNumBedrooms() + "\n" +
                "Bathrooms: " + getNumBathrooms() + "\n" +
                "Price: " + getPrice() + "\n";
    }
}
