using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Interactor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int neededItemID;
    public int rewardItemID;
    public void OnEnable()
    {
        if (DataManager.GetData().items[rewardItemID] > 0)
            gameObject.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Inventory.instance.selectedItemID == neededItemID)
        {
            Inventory.instance.interactMethod = CallbackFunction;
        }
    }
    public abstract void CallbackFunction();

    public void OnPointerExit(PointerEventData eventData)
    {
        Inventory.instance.interactMethod = null;
    }
}
