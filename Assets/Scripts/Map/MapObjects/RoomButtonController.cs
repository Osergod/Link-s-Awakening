using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomButtonController : MonoBehaviour
{
    [SerializeField] Sprite pressedSprite;
    [SerializeField] ActionableMapObject targetObject;

    private bool isPressed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !isPressed)
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
