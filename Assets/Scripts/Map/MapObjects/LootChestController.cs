using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootChestController : ActionableMapObject
{
    [SerializeField] bool isHidden = true;
    [SerializeField] Sprite openedSprite;
    [SerializeField] GameObject effect;
    [SerializeField] GameObject item;

    private bool canOpen;

    private void Update()
    {
        if (isHidden)
        {
            ShowChest(false);
        }
    }

    public void OpenChest()
    {
        if (canOpen)
        {
            canOpen = false;
            AudioManager.instance.PlaySFX(AudioManager.instance.chestOpen);
            gameObject.GetComponent<SpriteRenderer>().sprite = openedSprite;
        }
        
    }

    public override void Activate()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.roomSolved);
        Instantiate(effect, transform.position, Quaternion.identity);
        StartCoroutine(SpawnChest());
        isHidden = false;
    }

    public IEnumerator SpawnChest()
    {
        yield return new WaitForSeconds(0.20f);
        ShowChest(true);
    }

    public void ShowChest(bool state)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = state;
        gameObject.GetComponent<BoxCollider2D>().enabled = state;
        canOpen = state;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            OpenChest();
        }
    }
}
