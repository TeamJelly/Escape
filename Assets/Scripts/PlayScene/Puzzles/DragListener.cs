using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragListener : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public Transform targetImage;
    public void OnDrag(PointerEventData eventData)
    {
        targetImage.position = eventData.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        InventoryManager.instance.currentSelectItem = gameObject.name;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InventoryManager.instance.currentSelectItem = "";
        targetImage.localPosition = Vector3.zero;
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }


}
