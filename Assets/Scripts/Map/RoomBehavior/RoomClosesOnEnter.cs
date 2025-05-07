using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClosesOnEnter : MonoBehaviour
{
    [SerializeField] List<BlockedDoorController> roomDoors = new List<BlockedDoorController>();

    bool isSolved;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isSolved)
        {
            for (int i = 0; i < roomDoors.Count; i++)
            {
                roomDoors[i].Activate();
            }
        }
    }

    public void SolveRoom()
    {
        isSolved = true;
    }

    public void UnSolveRoom()
    {
        isSolved = false;
    }
}
