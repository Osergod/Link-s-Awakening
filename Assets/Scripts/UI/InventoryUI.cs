using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] 
    private InventoryUIItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    /*[SerializeField]
     private InventoryUIDescription itemDesc;*/

    [SerializeField]
    private MouseFolloweUI mouseFollower;

    List<InventoryUIItem> itemList = new List<InventoryUIItem>();

    public Sprite image;
    public int quantity;

    private void Awake()
    {
        Hide();
        mouseFollower.Toggle(false);
        /*itemDesc.ResetDescription();*/
    }

    public void InitializeInventoryUI(int inventorySize) { 
        for (int i = 0; i < inventorySize; i++)
        {
            InventoryUIItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            itemList.Add(uiItem);
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemDragged += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemDragEnd += HandleEndDrag;
            uiItem.OnRightMouseClick += HandleShowItemAtions;
        }
    }

    private void HandleShowItemAtions(InventoryUIItem item)
    {

    }

    private void HandleEndDrag(InventoryUIItem item)
    {
        mouseFollower.Toggle(false);
    }

    private void HandleSwap(InventoryUIItem item)
    {

    }

    private void HandleBeginDrag(InventoryUIItem item)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(image, quantity);
    }

    private void HandleItemSelection(InventoryUIItem item)
    {
        Debug.Log(item.name);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide() 
    {
        gameObject.SetActive(false);
    }
}
