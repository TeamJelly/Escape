﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Slot : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public Transform itemImage;
    public Text itemText;
    Item item;
    string puzzleName;

    public int GetItemID()
    {
        return item.ID;
    }
    public string GetItemName()
    {
        return item.itemName;
    }
    public void Init(Item i)
    {
        item = i;
        string itemName = ItemDatabase.GetItemWithID(item.ID).itemName;
        Sprite sprite = Resources.Load("Item/" + itemName, typeof(Sprite)) as Sprite;
        itemImage.GetComponent<Image>().sprite = sprite;
        if (sprite != null)
        {
            RectTransform newSize = (RectTransform)itemImage;
            Vector2 spriteSize = sprite.rect.size;
            if (spriteSize.x > spriteSize.y)
            {
                newSize.sizeDelta = new Vector2(100, spriteSize.y / spriteSize.x * 100);
            }
            else newSize.sizeDelta = new Vector2(spriteSize.x / spriteSize.y * 100, 100);
        }
        itemText.text = i.itemName;
    }
    public void OnDrag(PointerEventData data)
    {
        itemImage.position = data.position;
    }
    public void DropItem()
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Inventory.instance.selectedItem = item.itemName;
        itemImage.SetParent(itemImage.parent.parent.parent.parent);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Inventory.instance.selectedItem = " ";
        itemImage.SetParent(this.transform);
        itemImage.localPosition = Vector3.zero;
        Inventory.instance.interactMethod?.Invoke();
        Inventory.instance.interactMethod = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PuzzleManager.instance.StartPuzzleWithName(item.puzzle);
    }
}
