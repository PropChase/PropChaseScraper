package org.example.DataLayer;
import com.google.gson.Gson;
import com.mongodb.client.MongoClient;
import com.mongodb.client.MongoClients;
import com.mongodb.client.MongoCollection;
import com.mongodb.client.MongoDatabase;
import org.bson.Document;
import org.example.Listing.Listing;
import java.util.ArrayList;

public class MongoDB {
    private static MongoDB instance = new MongoDB();
    private static String connectionString = System.getenv("MONGODB_URI");

    private MongoDB() {
    }

    public static void postListings(ArrayList<Listing> listings, String site) {
        MongoClient mongoClient = MongoClients.create(connectionString);
        MongoDatabase database = mongoClient.getDatabase("Listings");
        MongoCollection<Document> collection = database.getCollection(site);

        Gson gson = new Gson();
        for (Listing listing : listings) {
            String json = gson.toJson(listing);
            Document doc = Document.parse(json);
            collection.insertOne(doc);
        }
    }

    public static void clearListings(String site) {
        MongoClient mongoClient = MongoClients.create(connectionString);
        MongoDatabase database = mongoClient.getDatabase("Listings");
        MongoCollection<Document> collection = database.getCollection(site);
        collection.deleteMany(new Document());
    }

    public static MongoDB getInstance() {
        return instance;
    }
}
