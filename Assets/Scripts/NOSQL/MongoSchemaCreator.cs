using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine;

public class MongoSchemaCreator : MonoBehaviour
{
    void Start()
    {
        ApplySchemaValidation();
    }

    void ApplySchemaValidation()
    {
        string connectionString = "mongodb+srv://Zelda:zelda69*@cluster0.tvouedw.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("Zelda");

        var command = new BsonDocument
        {
            {
                "collMod", "Stats_Game"
            },
            {
                "validator", new BsonDocument
                {
                    {
                        "$jsonSchema", new BsonDocument
                        {
                            { "bsonType", "object" },
                            { "required", new BsonArray { "PlayerName", "KillsNumber", "TimePlay" } },
                            { "properties", new BsonDocument
                                {
                                    { "PlayerName", new BsonDocument { { "bsonType", "string" } } },
                                    { "KillsNumber", new BsonDocument { { "bsonType", "int" } } },
                                    { "TimePlay", new BsonDocument { { "bsonType", "int" } } }
                                }
                            }
                        }
                    }
                }
            }
        };

        try
        {
            database.RunCommand<BsonDocument>(command);
            Debug.Log("Validació aplicada correctament a la col·lecció Stats_Game.");
        }
        catch (MongoCommandException ex)
        {
            Debug.LogWarning("No s'ha pogut aplicar la validació: " + ex.Message);
        }
    }
}
