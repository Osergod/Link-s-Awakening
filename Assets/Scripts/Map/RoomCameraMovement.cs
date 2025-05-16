using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomCameraMovement : MonoBehaviour
{
    SpriteRenderer[] objects;

    private void Start()
    {
        gameObject.GetComponent<TilemapRenderer>().enabled = false;
        objects = GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].enabled = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CameraController.instance.SetRoom(gameObject);
            CameraController.instance.MoveToRoom();
            gameObject.GetComponent<TilemapRenderer>().enabled = true;

            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] != null)
                {
                    objects[i].enabled = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameObject.GetComponent<TilemapRenderer>().enabled = false;

            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] != null)
                {
                    objects[i].enabled = false;
                }
            }
        }
    }
}
