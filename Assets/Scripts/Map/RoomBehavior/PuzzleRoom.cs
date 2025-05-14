using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PuzzleRoom : MonoBehaviour
{
    bool isSolved;

    [SerializeField] List<Enemy> roomEnemies = new List<Enemy>();

    private void Update()
    {
        if (roomEnemies.Count <= 0 && !isSolved)
        {
            SolveRoom();
        }
        else if (roomEnemies.Count > 0)
        {
            UnSolveRoom();
        }

        roomEnemies.RemoveAll(s => s == null);
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
