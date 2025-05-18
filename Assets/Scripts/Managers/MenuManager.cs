using MongoDB.Bson;
using TMPro;
using UnityEngine;
using System.Threading.Tasks;

public class MenuManager : MonoBehaviour
{
    private StatsManager stats;
    [SerializeField] private TMP_InputField playerNameInput;
    [SerializeField] private LinkDatabase db;
    private bool isDataSaved;

    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text killsText;
    [SerializeField] private TMP_Text playTimeText;
    [SerializeField] private TMP_Text victoryText;
    [SerializeField] private TMP_Text rupiasText;
    [SerializeField] private TMP_Text scoreText;

    void Start()
    {
        stats = StatsManager.Instance; // Accedim al singleton de StatsManager
    }

    // Mètode per establir el temps jugat
    public void SetPlayTime(int timeInSeconds)
    {
        stats.SetPlayTime(timeInSeconds);
    }

    // Funció per entrar el nom del jugador
    public async void OnEnterPlayerName()
    {
        if (!isDataSaved)
        {
            stats.SetPlayerName(playerNameInput.text);
            await db.UpdateOrInsertPlayerData();
            isDataSaved = true;
        }
    }

    // Carregar dades del jugador des de la base de dades
    public async void LoadPlayerDataFromDatabase()
    {
        var result = await db.LoadPlayerData(stats.GetPlayerName());

        if (result != null)
        {
            playerNameText.text = "Nombre: " + result["PlayerName"].AsString;
            killsText.text = "Kills: " + result["KillsNumber"].ToString();
            playTimeText.text = "Tiempo: " + FormatTime(int.Parse(result["TimePlay"].ToString()));
            victoryText.text = "Has ganado: " + (result["Victory"].AsBoolean ? "si" : "no");
            rupiasText.text = "Rupias: " + (result.Contains("Rupias") ? result["Rupias"].ToString() : "0");
            scoreText.text = "Puntuacion: " + (result.Contains("Score") ? result["Score"].ToString() : "0");
        }
    }

    // Formateig del temps en hores:minuts
    private string FormatTime(float totalSeconds)
    {
        int hours = Mathf.FloorToInt(totalSeconds / 3600);
        int minutes = Mathf.FloorToInt((totalSeconds % 3600) / 60);
        int seconds = Mathf.FloorToInt(totalSeconds % 60);
        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    // Funció que s'executa quan s'aconsegueix la victòria
    public void OnVictoryAchieved()
    {
        stats.SetVictory(true);
        if (victoryText != null)
            victoryText.text = "Has ganado: si";
    }
}
