using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchStairsActivation : MonoBehaviour
{
    [SerializeField] RoomStairsController stairsToActivate;
    [SerializeField] RoomStairsController stairsToDeactivate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        stairsToActivate.Activate();
        stairsToDeactivate.Deactivate();
    }
}
