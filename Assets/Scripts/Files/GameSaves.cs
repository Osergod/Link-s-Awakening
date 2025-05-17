using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameSaves : MonoBehaviour
{
    public static PlayerInfo PlayerInfo;

    public static void saveGameData(PlayerInfo game) 
    { 
        string json = JsonUtility.ToJson(game);
        File.WriteAllText(Application.persistentDataPath + "GameSavesFile.txt", json);

        Debug.Log(Application.persistentDataPath);
    }
}
