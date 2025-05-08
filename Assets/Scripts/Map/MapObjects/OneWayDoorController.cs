using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayDoorController : MonoBehaviour
{

    private Animator ator;

    private void Start()
    {
        ator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ator.SetTrigger("PlayerCrossed");
        }   
    }
}
