using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string playerName = "";

    private static GameManager gameManager;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

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
