using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalItem : MonoBehaviour
{
    [SerializeField]
    private PlayerInventory inventory;
    [SerializeField]
    private InventoryItem item;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.getItem);
            AddItemInventory();
            Destroy(this.gameObject);
        }
    }

    public void AddItemInventory() 
    {
        if (inventory && item) 
        {
            if (inventory.inventoryItems.Contains(item))
            {
                item.numberHeldItem++;
            }
            else 
            {
                inventory.inventoryItems.Add(item);
                item.numberHeldItem++;
            }
        }
    }
}
