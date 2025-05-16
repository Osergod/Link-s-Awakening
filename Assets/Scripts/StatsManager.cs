using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public string namePlayer;

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


}
