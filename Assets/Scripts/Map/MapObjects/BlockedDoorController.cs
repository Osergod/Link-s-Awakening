using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockedDoorController : ActionableMapObject
{
<<<<<<< HEAD
    [SerializeField] bool isActive = false;

    private Animator ator;

    private static bool playerEntered = false;

    enum DoorState { CLOSED, OPEN };
    DoorState doorState;
=======
    [SerializeField] Sprite openedSprite;

    private Animator ator;

    private bool isActive = false;

    enum DoorState { CLOSED, OPEN };
    DoorState doorState = DoorState.CLOSED;
>>>>>>> test

    private void Awake()
    {
        ator = GetComponent<Animator>();
<<<<<<< HEAD

        if (!isActive)
        {
            doorState = DoorState.CLOSED;
        }
        else
        {
            doorState = DoorState.OPEN;
        }
=======
>>>>>>> test
    }

    private void Update()
    {
        switch (doorState)
        {
            case DoorState.CLOSED:
                IsClosed();
                break;
            case DoorState.OPEN:
                IsOpen();
                break;
        }
    }

    public void IsClosed()
    {
<<<<<<< HEAD
        if (isActive)
        {
            doorState = DoorState.OPEN;
        }

        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        ator.SetBool("isActive", false);
=======
        if (!isActive)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            doorState = DoorState.OPEN;
        }
>>>>>>> test
    }

    public void IsOpen()
    {
<<<<<<< HEAD
        if (!isActive)
        {
            doorState = DoorState.CLOSED;
        }

        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        ator.SetBool("isActive", true);
=======
        if (isActive)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = openedSprite;
            ator.SetBool("isActive", true);
        }
        else
        {
            doorState = DoorState.CLOSED;
        }
>>>>>>> test
    }

    public override void Activate()
    {
        isActive = true;
    }
<<<<<<< HEAD

    public void Deactivate()
    {
        isActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerEntered = false;
        }
    }

    public void SetPlayerEntered(bool state)
    {
        playerEntered = state;
    }

    public bool GetPlayerEntered()
    {
        return playerEntered;
    }
=======
>>>>>>> test
}
