using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InfoReference : MonoBehaviour
{
    [SerializeField]
    PlayerID idPlayer;
    [SerializeField]
    PlayerName namePlayer;
    [SerializeField]
    PlayerScore scorePlayer;
    [SerializeField]
    PlayerRunTime runtimePlayer;

    PlayerInfo player = new PlayerInfo();

    private void Start()
    {
       // ScoreManager.instance.Player
    }

    public void saveData() 
    { 
        player.PlayerID = this.idPlayer.id;
        player.PlayerName = this.namePlayer.name;
        player.PlayerScore = this.scorePlayer.score;
        player.PlayerRunTime = this.runtimePlayer.runtime;
        GameSaves.saveGameData(player);
    }
}
