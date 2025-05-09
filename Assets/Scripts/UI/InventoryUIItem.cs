using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine.EventSystems;

public class InventoryUIItem : MonoBehaviour
{
    [SerializeField] 
    private Image itemImage;

    [SerializeField]
    private TMP_Text quantityTxt;

    public event Action<InventoryUIItem>  OnItemDragged, OnItemDragEnd, OnRightMouseClick, OnItemDroppedOn, OnItemClicked;

    private bool empty = true;

    public void Awake()
    {
        ResetData();
    }

    public void ResetData() { 
        this.itemImage.gameObject.SetActive(false);
        empty = true; 
    }

    public void SetData(Sprite sprite, int quantity) {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.quantityTxt.text = quantity + "";
        empty = false;
    }

    public void OnBeginDrag()
    {
        if(empty)
            return;
        OnItemDragged?.Invoke(this);
    }

    public void OnEndDrag()
    {
        OnItemDragEnd?.Invoke(this);
    }

    public void OnDrop() { 
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnPointerClick(BaseEventData data) {
        PointerEventData pointerData = (PointerEventData)data;
        if (empty)
            return;
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseClick?.Invoke(this);
        }
        else { 
            OnItemClicked?.Invoke(this);
        }
    }
}