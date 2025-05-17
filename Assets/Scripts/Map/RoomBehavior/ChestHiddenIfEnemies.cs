using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestHiddenIfEnemies : PuzzleRoom
{
    [SerializeField] List<LootChestController> roomChests = new List<LootChestController>();

    public override void SolveRoom()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.roomSolved);
        SetIsSolved(true);
        ActivateChests();
    }

    public override void UnSolveRoom()
    {
        SetIsSolved(false);
    }

    private void ActivateChests()
    {
        for (int i = 0; i < roomChests.Count; i++)
        {
            roomChests[i].Activate();
        }
    }
}
