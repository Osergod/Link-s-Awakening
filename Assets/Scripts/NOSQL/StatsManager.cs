using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public string playerName;
    public int killCount;
    public int playTime;
    public bool victory;
    public int rupias;
    public int score;

    public static StatsManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (GameManager.instance != null)
        {
            killCount = GameManager.instance.GetPlayerKills();
            score = GameManager.instance.GetScore();
        }
    }

    // Getters y setters de las estadisticas a guardar en la base de datos
    public string GetPlayerName() => playerName;
    public void SetPlayerName(string newName) => playerName = newName;
    public int GetKillCount() => killCount;
    public void SetKillCount(int newKills) => killCount = newKills;
    public int GetPlayTime() => playTime;
    public void SetPlayTime(int newTime) => playTime = newTime;
    public bool GetVictory() => victory;
    public void SetVictory(bool v) => victory = v;
    public int GetRupias() => rupias;
    public void SetRupias(int value) => rupias = value;
    public int GetScore() => score;
    public void SetScore(int newScore) => score = newScore;
}