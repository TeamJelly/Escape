using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Slot : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public GameObject PopUp;
    public Transform imageTransform;

    public void Start()
    {
        Sprite sprite = imageTransform.gameObject.GetComponent<Image>().sprite;
        if (sprite != null)
        {
            RectTransform newSize = (RectTransform)imageTransform;
            Vector2 spriteSize = sprite.rect.size;
            if (spriteSize.x > spriteSize.y)
                newSize.sizeDelta = new Vector2(180, spriteSize.y / spriteSize.x * 180);
            else
                newSize.sizeDelta = new Vector2(spriteSize.x / spriteSize.y * 180, 180);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        imageTransform.position = eventData.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        InventoryManager.instance.currentSelectItem = gameObject.name;
        imageTransform.SetParent(transform.parent.parent.parent.parent);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InventoryManager.instance.currentSelectItem = "";
        imageTransform.SetParent(transform);
        imageTransform.localPosition = Vector3.zero;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (!PlayUIManager.instance.PopUpPanel.activeSelf)
            PlayUIManager.instance.SetPopUp(PopUp);
    }
}
