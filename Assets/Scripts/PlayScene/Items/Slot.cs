using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Slot : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public int itemID;
    public Transform itemImage;

    public void Init(int id)
    {
        itemID = id;
        string itemName = ItemDatabase.GetItemWithID(itemID).itemName;
        itemImage.GetComponent<Image>().sprite = Resources.Load("Item/" + itemName,typeof(Sprite)) as Sprite;
    }
    public void OnDrag(PointerEventData data)
    {
        itemImage.position = data.position;
    }

    public void OnEndDrag(PointerEventData data)
    {
        
    }
    public void DropItem()
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Inventory.instance.selectedItemID = itemID;
        itemImage.SetParent(itemImage.parent.parent.parent.parent);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Inventory.instance.selectedItemID = -1;
        itemImage.SetParent(this.transform);
        itemImage.localPosition = Vector3.zero;
        Inventory.instance.interactMethod?.Invoke();
        Inventory.instance.interactMethod = null;
    }
}
