using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject room;
    [SerializeField] float speed = 0.05f;

    GameObject oldRoom;
    

    enum CAMERA_STATES { IDLE, MOVE_TO_ROOM };
    CAMERA_STATES cameraState = CAMERA_STATES.IDLE;

    public static CameraController cameraController;
    public static CameraController instance
    {
        get
        {
            return RequestCameraController();
        }

    }

    private void Update()
    {
        switch (cameraState)
        {
            case CAMERA_STATES.IDLE:
                break;
            case CAMERA_STATES.MOVE_TO_ROOM:
                if (transform.position.x != room.transform.position.x || transform.position.y != room.transform.position.y)
                {
                    float oldZ = transform.position.z;
                    transform.position = Vector3.Lerp(oldRoom.transform.position, room.transform.position, speed);
                    transform.position = new Vector3(transform.position.x, transform.position.y, oldZ);
                }
                else
                {
                    cameraState = CAMERA_STATES.IDLE;
                }
                break;
        }
    }

    public void MoveToRoom()
    {
        cameraState = CAMERA_STATES.MOVE_TO_ROOM;
    }

    public static CameraController RequestCameraController()
    {
        if (!cameraController)
        {
            cameraController = FindObjectOfType<CameraController>();
        }
        return cameraController;
    }

    public void SetRoom(GameObject room)
    {
        oldRoom = this.room;
        this.room = room;
    }
}
