using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Player items")]
public class PlayerInventory : ScriptableObject
{
    public List<InventoryItem> inventoryItems = new();

}
