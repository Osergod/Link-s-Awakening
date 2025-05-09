using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomButtonController : MonoBehaviour
{
    [SerializeField] Sprite pressedSprite;
<<<<<<< HEAD
    [SerializeField] ActionableMapObject targetObject;
=======
    [SerializeField] BlockedDoorController targetObject;
>>>>>>> test

    private bool isPressed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
<<<<<<< HEAD
        if(collision.tag == "Player" && !isPressed)
=======
        if(collision.tag == "Player")
>>>>>>> test
        {
            isPressed = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = pressedSprite;
            targetObject.Activate();
        }
    }

    public bool GetIsPressed()
    {
        return isPressed;
    }
}
