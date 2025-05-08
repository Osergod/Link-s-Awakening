using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClosedIfEnemies : PuzzleRoom
{
    [SerializeField] List<BlockedDoorController> roomDoors = new List<BlockedDoorController>();

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(GetIsSolved());
        if (collision.tag == "Player" && !GetIsSolved())
        {
            for (int i = 0; i < roomDoors.Count; i++)
            {
                if (!roomDoors[i].GetPlayerEntered())
                {
                    roomDoors[i].Deactivate();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !GetIsSolved())
        {
            ActivateDoors();
        }
    }

    private void ActivateDoors()
    {
        for (int i = 0; i < roomDoors.Count; i++)
        {
            roomDoors[i].Activate();
        }
    }

    public override void SolveRoom()
    {
        SetIsSolved(true);
        ActivateDoors();
    }

    public override void UnSolveRoom()
    {
        SetIsSolved(false);
    }
}
