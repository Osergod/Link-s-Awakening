using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockedDoorController : ActionableMapObject
{
    [SerializeField] bool isActive = false;

    private Animator ator;

    

    enum DoorState { CLOSED, OPEN };
    DoorState doorState = DoorState.CLOSED;

    private void Awake()
    {
        ator = GetComponent<Animator>();
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
        if (!isActive)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            ator.SetBool("isActive", false);
        }
        else
        {
            doorState = DoorState.OPEN;
        }
    }

    public void IsOpen()
    {
        if (isActive)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            ator.SetBool("isActive", true);
        }
        else
        {
            doorState = DoorState.CLOSED;
        }
    }

    public override void Activate()
    {
        isActive = true;
    }
}
