using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string playerName = "";
    [SerializeField] private int playerKills;
    [SerializeField] private int score;
    [SerializeField] private int keys = 0;
    [SerializeField] private int bombs = 0;

    [Header("Items For Inventory")]
    [SerializeField] private InventoryItem keyItem;
    [SerializeField] private InventoryItem bombItem;

    private bool playerJumping = false;
    private bool hasMap = false;

    private static GameManager gameManager;

    private void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(gameObject);
            return;
        }

        gameManager = this;
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

    private void Update()
    {
        keyItem.numberHeldItem = keys;
        bombItem.numberHeldItem = bombs;
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

    public void IncrementKills()
    {
        playerKills++;
    }

    public int GetPlayerKills()
    {
        return playerKills;
    }

    public int GetScore()
    {
        return score;
    }

    public void IncrementScore(int newScore)
    {
        score += newScore;
    }

    public void IncrementKeys()
    {
        keys ++;
    }

    public void DecrementKeys()
    {
        keys--;
    }

    public int GetKeys()
    {
        return keys;
    }

    public void IncrementBombs()
    {
        bombs += 5;
    }

    public void DecrementBombs()
    {
        bombs--;
    }

    public int GetBombs()
    {
        return bombs;
    }

    public bool GetPlayerJumping()
    {
        return playerJumping;
    }

    public void SetPlayerJumping(bool playerJumping)
    {
        this.playerJumping = playerJumping;
    }

    public void SetHasMap(bool hasMap)
    {
        this.hasMap = hasMap;
    }

    public bool GetHasMap()
    {
        return hasMap;
    }

    public int GetItemForInventory(string item)
    {
        switch (item)
        {
            case "keys":
                return keys;
                break;
            case "bombs":
                return bombs;
        }

        return 0;
    }
}
