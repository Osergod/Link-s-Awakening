using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClosesOnEnter : MonoBehaviour
{
    [SerializeField] List<BlockedDoorController> roomDoors = new List<BlockedDoorController>();
    [SerializeField] List<BlockedDoorController> roomEnemies = new List<BlockedDoorController>();

    bool isSolved;

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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isSolved)
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
        if (collision.tag == "Player" && !isSolved)
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

    public void SolveRoom()
    {
        isSolved = true;
        ActivateDoors();
    }

    public void UnSolveRoom()
    {
        isSolved = false;
    }
}
