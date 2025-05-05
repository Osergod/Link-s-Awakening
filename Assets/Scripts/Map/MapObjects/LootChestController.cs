using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootChestController : ActionableMapObject
{
    [SerializeField] bool isHidden = true;
    [SerializeField] Sprite openedSprite;

    private void Update()
    {
        if (isHidden)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void OpenChest()
    {
        
    }

    public override void Activate()
    {
        isHidden = false;
    }
}
