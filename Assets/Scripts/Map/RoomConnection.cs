using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomConnectionController : MonoBehaviour
{
    [SerializeField]
    GameObject destination;
    [SerializeField]
    Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetTrigger("Start");
        collision.transform.position = destination.transform.position;
        anim.SetTrigger("End");
    }
}
