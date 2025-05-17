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
    [SerializeField] private TMP_Text victoryText;
    [SerializeField] private TMP_Text rupiasText;

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

        var filter = Builders<BsonDocument>.Filter.Eq("PlayerName", stats.GetPlayerName());
        var result = await usersCollection.Find(filter).FirstOrDefaultAsync();

        if (result != null)
        {
            string name = result["PlayerName"].AsString;
            string kills = result["KillsNumber"].ToString();
            string time = result["TimePlay"].ToString();

            bool victory = result.Contains("Victory") && result["Victory"].IsBoolean ? result["Victory"].AsBoolean : false;

            playerNameText.text = "Nombre: " + name;
            killsText.text = "Kills: " + kills;
            int totalSeconds = int.Parse(time);
            playTimeText.text = "Tiempo: " + FormatTime(totalSeconds);
            if (victoryText != null)
                victoryText.text = "Has ganado: " + (victory ? "sí" : "no");
            string rupias = result.Contains("Rupias") ? result["Rupias"].ToString() : "0";
            if (rupiasText != null)
                rupiasText.text = "Rupias: " + rupias;
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

    public void OnVictoryAchieved()
    {
        stats.SetVictory(true);
        Debug.Log("Victory achieved!");
        if (victoryText != null)
            victoryText.text = "Has ganado: sí";
    }
}
