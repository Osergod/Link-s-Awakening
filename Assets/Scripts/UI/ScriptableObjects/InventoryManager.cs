using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InventoryManager : MonoBehaviour
{
    //Invetory Info 
    [SerializeField]
    private GameObject blankSlot;
    [SerializeField]
    private GameObject inventoryPanel;
    [SerializeField]
    private TextMeshProUGUI textDesc;
    [SerializeField]
    private GameObject useButton;

    public PlayerInventory playerInventory;
    public InventoryItem currentItem;

    public void SetTextAButton(string desc, bool activeButton)
    {
        textDesc.text = desc;
        if (activeButton)
        {
            useButton.SetActive(true);
        }
        else
        {
            useButton.SetActive(false);
        }
    }

    public void CreateSlots()
    {
        if (playerInventory)
        {
            for (int i = 0; i < playerInventory.inventoryItems.Count; i++)
            {
                if (playerInventory.inventoryItems[i].numberHeldItem > 0) {
                    GameObject temp = Instantiate(blankSlot, inventoryPanel.transform.position, Quaternion.identity);
                    temp.transform.SetParent(inventoryPanel.transform);
                    InventorySlots newSlot = temp.GetComponent<InventorySlots>();
                    if (newSlot)
                    {
                        newSlot.SetUp(playerInventory.inventoryItems[i], this);
                    }
                }
            }
        }
    }
    void OnEnable()
    {
        CreateSlots();
        Debug.Log("Inventory");
        SetTextAButton("", false);
    }

    public void SetupDescAndButton(string newDesc, bool isUsable, InventoryItem newItem)
    {
        currentItem = newItem;
        textDesc.text  = newDesc;
        useButton.SetActive(isUsable);   
    }

    public void ClearSlots() 
    {
        for (int i = 0; i < inventoryPanel.transform.childCount; i++) 
        { 
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }
    }

    public void UseButtonPress() {
        if (currentItem) {
            currentItem.Use();
            ClearSlots();
            CreateSlots();
            SetTextAButton("", false );
        }
    }
}
