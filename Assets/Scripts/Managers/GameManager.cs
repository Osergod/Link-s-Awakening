using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private string playerName = "";

    private static GameManager gameManager;

    public static GameManager instance
    {
        get {
            return RequestGameManager();
        }
    }

    private static GameManager RequestGameManager()
    {
        if (!gameManager)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
        return gameManager;
    }

    public void SetPlayerName(string newName)
    {
        playerName = newName;
        Debug.Log("New Name is: " + newName);
    }

    public string GetPlayerName()
    {
        return playerName;
    }
}
