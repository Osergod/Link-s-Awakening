using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine;

public class LinkDatabase : MonoBehaviour
{
    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<BsonDocument> usersCollection;

    public void UpdateDatabase()
    {
        string connectionString = "mongodb+srv://Zelda:zelda69*@cluster0.tvouedw.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";

        try
        {
            client = new MongoClient(connectionString);
            database = client.GetDatabase("Zelda");
            usersCollection = database.GetCollection<BsonDocument>("Stats_Game");
        }
        catch (System.Exception e)
        {
            Debug.Log("MongoDB Connection Error: " + e.Message);
            return;
        }

        if (StatsManager.Instance == null)
        {
            Debug.LogError("StatsManager.Instance is null!");
            return;
        }

        var document = new BsonDocument
        {
            { "PlayerName",StatsManager.Instance.GetPlayerName() },
            { "KillsNumber",StatsManager.Instance.killCount },
            { "TimePlay",StatsManager.Instance.playTime },
            { "Victory",StatsManager.Instance.victory }
        };

        usersCollection.InsertOne(document);
        Debug.Log("Document inserted correctly into MongoDB.");
    }
}
