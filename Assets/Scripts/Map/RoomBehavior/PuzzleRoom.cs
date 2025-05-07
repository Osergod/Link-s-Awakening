using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PuzzleRoom : MonoBehaviour
{
    bool isSolved;

    [SerializeField] List<BlockedDoorController> roomEnemies = new List<BlockedDoorController>();

    private void Update()
    {
        if (roomEnemies.Count == 0 && !isSolved)
        {
            SolveRoom();
        }
        else
        {
            UnSolveRoom();
        }
    }

    public abstract void SolveRoom();

    public abstract void UnSolveRoom();

    public bool GetIsSolved()
    {
        return isSolved;
    }

    public void SetIsSolved(bool state)
    {
        isSolved = state;
    }
}
