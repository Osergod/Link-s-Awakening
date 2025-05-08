using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoorController : ActionableMapObject
{
    Animator ator;

    private void Start()
    {
        ator = GetComponent<Animator>();
    }

    public override void Activate()
    {
        ator.SetTrigger("Activated");
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<LinkController>().GetKeys() > 0)
            {
                Activate();
                collision.GetComponent<LinkController>().DecrementKeys();
            }
        }
    }
}
