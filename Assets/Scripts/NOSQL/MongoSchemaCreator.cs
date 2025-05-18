using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine;
using System.Threading.Tasks;

public class MongoSchemaCreator : MonoBehaviour
{
    public async Task ApplySchemaValidation()
    {
        string connectionString = "mongodb+srv://Zelda:zelda69*@cluster0.tvouedw.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("Zelda");

        var command = new BsonDocument
        {
            { "collMod", "Stats_Game" },
            { "validator", new BsonDocument
                {
                    { "$jsonSchema", new BsonDocument
                        {
                            { "bsonType", "object" },
                            { "required", new BsonArray { "PlayerName", "KillsNumber", "TimePlay", "Victory", "Rupias", "Score" } },
                            { "properties", new BsonDocument
                                {
                                    { "PlayerName", new BsonDocument { { "bsonType", "string" } } },
                                    { "KillsNumber", new BsonDocument { { "bsonType", "int" } } },
                                    { "TimePlay", new BsonDocument { { "bsonType", "int" } } },
                                    { "Victory", new BsonDocument { { "bsonType", "bool" } } },
                                    { "Rupias", new BsonDocument { { "bsonType", "int" } } },
                                    { "Score", new BsonDocument { { "bsonType", "int" } } }
                                }
                            }
                        }
                    }
                }
            }
        };

        try
        {
            await database.RunCommandAsync<BsonDocument>(command);
            Debug.Log("Schema aplicado correctamente");
        }
        catch (MongoCommandException ex)
        {
            Debug.LogWarning("El schema ya estaba aplicado: " + ex.Message);
        }
    }
}