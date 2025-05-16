using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        }

        var document = new BsonDocument
        {
            { "PlayerName",StatsManager.Instance.GetNamePlayer() },
            // { "KillsNumber",StatsManager.Instance.GetNumberKills() },
            { "KillsNumber","3" },
            //{ "TimePlay",StatsManager.Instance.GetPlayTime() },
            { "TimePlay","3600" },
        };

        usersCollection.InsertOne(document);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
