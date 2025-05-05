using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootChestController : ActionableMapObject
{
    [SerializeField] bool isHidden = true;
    [SerializeField] Sprite openedSprite;
    [SerializeField] GameObject effect;

    private void Update()
    {
        if (isHidden)
        {
            ShowChest(false);
        }
    }

    public void OpenChest()
    {
        
    }

    public override void Activate()
    {
        Instantiate(effect, transform.position, Quaternion.identity);
        StartCoroutine(SpawnChest());
        isHidden = false;
    }

    public IEnumerator SpawnChest()
    {
        yield return new WaitForSeconds(0.20f);
        ShowChest(true);
    }

    public void ShowChest(bool s)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = s;
        gameObject.GetComponent<BoxCollider2D>().enabled = s;
    }
}
