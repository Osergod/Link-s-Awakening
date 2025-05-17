using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public string playerName;
    public int killCount;
    public int playTime;
    public bool victory;
    public int rupias;

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

        victory = false;
    }

    private void Update()
    {
        playTime = Mathf.FloorToInt(Time.timeSinceLevelLoad);
    }

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

}
