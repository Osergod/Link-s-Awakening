using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class RoomWithEnemies : MonoBehaviour
{
    Enemy[] enemies;
    BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        boxCollider.size = new Vector2(10, 8); 
    }

    private void Start()
    {
        enemies = GetComponentsInChildren<Enemy>();

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].Deactivate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i] != null)
                {
                    enemies[i].Activate();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i] != null)
                {
                    enemies[i].Deactivate();
                }
            }
        }
    }
}
