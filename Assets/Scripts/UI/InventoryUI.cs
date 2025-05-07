using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] 
    private InventoryUIItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    List<InventoryUIItem> itemList = new List<InventoryUIItem>();

    public void InitializeInventoryUI(int inventorySize) { 
        for (int i = 0; i < inventorySize; i++)
        {
            InventoryUIItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            itemList.Add(uiItem);
        }
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
