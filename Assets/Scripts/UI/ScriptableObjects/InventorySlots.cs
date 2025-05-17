using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using Microsoft.Unity.VisualStudio.Editor;


public class InventorySlots : MonoBehaviour
{
    //UI info 
    [SerializeField]
    private TextMeshProUGUI itemNumberText;
    [SerializeField]
    public UnityEngine.UI.Image itemImage;

    //Item variables
    /*public Sprite itemSprite;
    public int numHeld;
    public string itemDesc;*/
    public InventoryItem thisItem;
    public InventoryManager thisManager;

    public void SetUp(InventoryItem newItem, InventoryManager newManager)
    {
        thisItem = newItem;
        thisManager = newManager;
        if (thisItem)
        {
            itemImage.sprite = thisItem.itemImage;
            itemNumberText.text = "" + thisItem.numberHeldItem;
        }
    }

    public void ClickButton()
    {
        if (thisItem)
        {
            thisManager.SetupDescAndButton(thisItem.itemDesc, thisItem.usableItem, thisItem);
        }
    }

}
