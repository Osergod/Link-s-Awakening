using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomButtonController : MonoBehaviour
{
    [SerializeField] Sprite pressedSprite;

    private bool isPressed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isPressed = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = pressedSprite;
        }
    }

    public bool GetIsPressed()
    {
        return isPressed;
    }
}
