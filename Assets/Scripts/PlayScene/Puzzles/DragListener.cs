using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragListener : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public string objName;
    public Transform targetImage;
    public void OnDrag(PointerEventData eventData)
    {
        targetImage.position = eventData.position;
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Inventory.instance.selectedItem = objName;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.transform.position = targetImage.position;
        targetImage.localPosition = Vector3.zero;
        Inventory.instance.interactMethod?.Invoke();
        Inventory.instance.interactMethod = null;
        Inventory.instance.selectedItem = " ";
    }
}
