using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine;
using System.Threading.Tasks;

public class LinkDatabase : MonoBehaviour
{
    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<BsonDocument> usersCollection;

    private async void Start()
    {
        await InitializeDatabase();
    }

    private async Task InitializeDatabase()
    {
        string connectionString = "mongodb+srv://Zelda:zelda69*@cluster0.tvouedw.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";

        try
        {
            client = new MongoClient(connectionString);
            database = client.GetDatabase("Zelda");
            usersCollection = database.GetCollection<BsonDocument>("Stats_Game");

            MongoSchemaCreator schemaCreator = FindObjectOfType<MongoSchemaCreator>();
            if (schemaCreator != null)
            {
                await schemaCreator.ApplySchemaValidation();
            }
        }
        catch (MongoException ex)
        {
            Debug.LogError("Error de MongoDB: " + ex.Message);
        }
    }

    public async Task UpdateOrInsertPlayerData()
    {
        if (usersCollection == null)
        {
            await InitializeDatabase();
        }

        var filter = Builders<BsonDocument>.Filter.Eq("PlayerName", StatsManager.Instance.GetPlayerName());
        var document = new BsonDocument
        {
            { "PlayerName", StatsManager.Instance.GetPlayerName() },
            { "KillsNumber", StatsManager.Instance.GetKillCount() },
            { "TimePlay", StatsManager.Instance.GetPlayTime() },
            { "Victory", StatsManager.Instance.GetVictory() },
            { "Rupias", StatsManager.Instance.GetRupias() },
            { "Score", StatsManager.Instance.GetScore() }
        };

        try
        {
            await usersCollection.ReplaceOneAsync(
                filter,
                document,
                new ReplaceOptions { IsUpsert = true }
            );
        }
        catch (MongoWriteException ex) when (ex.WriteError.Code == 121)
        {
            Debug.LogError("Validacion fallida: " + ex.WriteError.Message);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error al guardar: " + ex.Message);
        }
    }

    public async Task<BsonDocument> LoadPlayerData(string playerName)
    {
        try
        {
            var filter = Builders<BsonDocument>.Filter.Eq("PlayerName", playerName);
            return await usersCollection.Find(filter).FirstOrDefaultAsync();
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error al cargar datos: " + ex.Message);
            return null;
        }
    }
}