using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public string namePlayer;
    public int numberKills;
    public float playTime;

    public static StatsManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public string GetNamePlayer()
    {
        return namePlayer;
    }

    public void SetNamePlayer(string newNamePlayer)
    {
        namePlayer = newNamePlayer;
    }
    /*
    public int GetNumberKills()
    {
        return numberKills;
    }

    public void SetNumberKills(int newKills)
    {
        numberKills = newKills;
    }
    */
   /* public float GetPlayTime()
    {
        return playTime;
    }

    public void SetPlayTime(float newTime)
    {
        playTime = newTime;
    } */
}
