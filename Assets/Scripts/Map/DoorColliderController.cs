using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorColliderController : MonoBehaviour
{
    [SerializeField]
    GameObject destination;

    void Start()
    {
        Vector3 destLocation = destination.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = destination.transform.position + new Vector3 (0, destination.transform.localScale.y + 1, 0);
    }
}
