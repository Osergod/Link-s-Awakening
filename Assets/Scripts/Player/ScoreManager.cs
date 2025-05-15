using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager scoreManger;

    public static ScoreManager instance
    {
        get
        {
            return RequestGameManager();
        }
    }

    private static ScoreManager RequestGameManager()
    {
        if (!scoreManger)
        {
            scoreManger = FindObjectOfType<ScoreManager>();
        }
        return scoreManger;
    }

    [SerializeField]
    private PlayerID idPlayers;
    [SerializeField]
    private PlayerName usernamePlayers;
    [SerializeField]
    private PlayerScore scorePlayers;
    [SerializeField]
    private PlayerRunTime runtimePlayers;


    /* //Resetear Score
    public void ResetMoney(bool save = true)
    {
        PlayerPrefs.DeleteKey(SAVEGAMEKEY_MONEY);
        if (save)
        {
            PlayerPrefs.Save();
        }

        LoadMoney();

    }*/
}
