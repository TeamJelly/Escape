using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Interactor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public int neededItemID;
    
    public string onClickMessage;
    public void OnEnable()
    {
        if(CheckFinished())
            gameObject.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Inventory.instance.selectedSlot?.itemID == neededItemID)
        {
            Slot temp = Inventory.instance.selectedSlot;
            Inventory.instance.interactMethod = ()=> CallbackFunction(temp);
        }
    }
    public abstract bool CheckFinished();
    public abstract void CallbackFunction(Slot slot);
    public abstract void OnClickFunction();

    public void OnPointerExit(PointerEventData eventData)
    {
        Inventory.instance.interactMethod = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        ChatSystem2.instance.Monologue(onClickMessage);
        OnClickFunction();
       
    }
}
