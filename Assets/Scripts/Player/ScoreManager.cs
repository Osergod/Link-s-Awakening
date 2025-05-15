using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

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
    PlayerID idPlayers;
    [SerializeField]
    private PlayerName usernamePlayers;
    [SerializeField]
    private PlayerScore scorePlayers;
    [SerializeField]
    private PlayerRunTime runtimePlayers;

    /* 
    //Reset Score
    public void ResetMoney(bool save = true)
    {
        PlayerPrefs.DeleteKey(SAVEGAMEKEY_MONEY);
        if (save)
        {
            PlayerPrefs.Save();
        }

        LoadMoney();

    }

    //Save score
    public void SaveMoney(bool save = true)
    {
        PlayerPrefs.SetFloat(SAVEGAMEKEY_MONEY, banck);

        if (save)
        {
            PlayerPrefs.Save();
        }
    }

    //Load score
    public void LoadMoney()
    {
        banck = PlayerPrefs.GetFloat(SAVEGAMEKEY_MONEY, 0);
    }*/
}
