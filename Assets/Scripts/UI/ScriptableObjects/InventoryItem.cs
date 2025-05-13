using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
public class InventoryItem : ScriptableObject
{

    public UnityEngine.Events.UnityEvent thisEvent = new UnityEngine.Events.UnityEvent();
    public string nameItem;
    public string itemDesc;
    public Sprite itemImage;
    public int numberHeldItem;
    public bool usableItem;
    public bool uniqueItem; 

    public void Use() { 
        thisEvent.Invoke();
        Debug.Log("using Item");
    }
}
