using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject room;

    public static CameraController cameraController;

    public static CameraController instance
    {
        get
        {
            return RequestCameraController1();
        }
        
    }

    public static CameraController RequestCameraController1()
    {
        if (!cameraController)
        {
            cameraController = FindObjectOfType<CameraController>();
        }
        return cameraController;
    }

void Update()
    {
        transform.position = new Vector3 (room.transform.position.x, room.transform.position.y, transform.position.z);
    }

    public void SetRoom(GameObject room)
    {
        this.room = room;
    }
}
