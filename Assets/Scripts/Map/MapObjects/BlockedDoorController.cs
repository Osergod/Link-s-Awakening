using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockedDoorController : MonoBehaviour
{
    [SerializeField] Sprite openedSprite;

    private Animator ator;

    private bool isActive = false;

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
            gameObject.GetComponent<SpriteRenderer>().sprite = openedSprite;
            ator.SetBool("isActive", true);
        }
        else
        {
            doorState = DoorState.CLOSED;
        }
    }

    public void Activate()
    {
        isActive = true;
    }
}
