using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomCameraMovement : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<TilemapRenderer>().enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CameraController.instance.SetRoom(gameObject);
            CameraController.instance.MoveToRoom();
            gameObject.GetComponent<TilemapRenderer>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponent<TilemapRenderer>().enabled = false;
    }
}
