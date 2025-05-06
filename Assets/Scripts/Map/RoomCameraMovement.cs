using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCameraMovement : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CameraController.instance.SetRoom(gameObject);
            CameraController.instance.MoveToRoom();
        }
    }
}
