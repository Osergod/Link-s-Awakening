using System.Collections;
using MongoDB.Bson;
using MongoDB.Driver;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private StatsManager stats;
    [SerializeField] private TMP_InputField playerNameInput;
    [SerializeField] private LinkDatabase db;

    private bool dbSaved;
    int total;

    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text killsText;
    [SerializeField] private TMP_Text playTimeText;

    void Start()
    {
        stats = FindObjectOfType<StatsManager>();
        dbSaved = false;
    }

    public void OnEnterPlayerName()
    {
        if (!dbSaved)
        {
            stats.playerName = playerNameInput.text;
            Debug.Log("Saved player name: " + stats.playerName);
            stats.playTime = Mathf.FloorToInt(Time.time);
            db.UpdateDatabase();
            dbSaved = true;
        }
        else
        {
            OnChangedPlayerName();
        }
    }

    public void OnChangedPlayerName()
    {
        if (dbSaved)
        {
            playerNameText.text = "You can only save your data once";
            Debug.Log("You can only save your data once");
        }
    }

    public async void LoadPlayerDataFromDatabase()
    {
        if (stats == null)
            stats = FindObjectOfType<StatsManager>();

        string connectionString = "mongodb+srv://Zelda:zelda69*@cluster0.tvouedw.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("Zelda");
        var usersCollection = database.GetCollection<BsonDocument>("Stats_Game");

        var filter = Builders<BsonDocument>.Filter.Eq("PlayerName", stats.playerName);
        var result = await usersCollection.Find(filter).FirstOrDefaultAsync();

        if (result != null)
        {
            string name = result["PlayerName"].AsString;
            string kills = result["KillsNumber"].ToString();
            string time = result["TimePlay"].ToString();

            playerNameText.text = "Player Name: " + name;
            killsText.text = "Number of Kills: " + kills;
            int totalSeconds = int.Parse(time);
            playTimeText.text = "Time: " + FormatTime(totalSeconds);
        }
        else
        {
            playerNameText.text = "Player not found";
            Debug.LogWarning("Player name not found in the database.");
        }
    }

    private string FormatTime(float totalSeconds)
    {
        int hours = Mathf.FloorToInt(totalSeconds / 3600);
        int minutes = Mathf.FloorToInt((totalSeconds % 3600) / 60);
        int seconds = Mathf.FloorToInt(totalSeconds % 60);

        return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
    }
}
