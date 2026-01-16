using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Item : MonoBehaviour
{
    private Sprite sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
    }

    public abstract void Use();
}
