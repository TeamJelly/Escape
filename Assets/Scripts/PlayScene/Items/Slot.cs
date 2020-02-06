using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Slot : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Transform itemImage;
    Item item;


    public int GetItemID()
    {
        return item.ID;
    }
    public void Init(Item i)
    {
        item = i;
        string itemName = ItemDatabase.GetItemWithID(item.ID).itemName;
        itemImage.GetComponent<Image>().sprite = Resources.Load("Item/" + itemName,typeof(Sprite)) as Sprite;
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
        Inventory.instance.selectedSlot = this;
        itemImage.SetParent(itemImage.parent.parent.parent.parent);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Inventory.instance.selectedSlot = null;
        itemImage.SetParent(this.transform);
        itemImage.localPosition = Vector3.zero;
        Inventory.instance.interactMethod?.Invoke();
        Inventory.instance.interactMethod = null;
    }
}
