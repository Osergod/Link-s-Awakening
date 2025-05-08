using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feather : MonoBehaviour
{
    private LinkController link;

    private void Start()
    {
        link.HasFeather = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        link.HasFeather = true;
        Destroy(gameObject);
    }
}
