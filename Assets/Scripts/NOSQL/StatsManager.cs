using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public string playerName;
    public int killCount;
    public int playTime;

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
        playTime = Mathf.FloorToInt(Time.timeSinceLevelLoad);
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public void SetPlayerName(string newName)
    {
        playerName = newName;
    }

    public int GetKillCount()
    {
        return killCount;
    }

    public void SetKillCount(int newKills)
    {
        killCount = newKills;
    }

    public int GetPlayTime()
    {
        return playTime;
    }

    public void SetPlayTime(int newTime)
    {
        playTime = newTime;
    }
}
