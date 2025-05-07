using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockedDoorController : ActionableMapObject
{
    [SerializeField] bool isActive = false;

    private Animator ator;

    enum DoorState { CLOSED, OPEN };
    DoorState doorState;

    private void Awake()
    {
        ator = GetComponent<Animator>();

        if (!isActive)
        {
            doorState = DoorState.CLOSED;
        }
        else
        {
            doorState = DoorState.OPEN;
        }
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
        if (isActive)
        {
            doorState = DoorState.OPEN;
        }

        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        ator.SetBool("isActive", false);
    }

    public void IsOpen()
    {
        if (!isActive)
        {
            doorState = DoorState.CLOSED;
        }

        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        ator.SetBool("isActive", true);
    }

    public override void Activate()
    {
        if (!isActive)
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }
    }
}
