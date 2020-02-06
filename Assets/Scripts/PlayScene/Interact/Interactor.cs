using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Interactor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Serializable]
    public class MyEvent : UnityEvent { }

    [Tooltip("이 퍼즐을 풀기위해 필요한 아이템 이름")]
    public string neededItem;

    [Tooltip("해당 이벤트 이름")]
    public string thisEvent;

    public MyEvent OnFailed;
    public MyEvent OnClick;
    public MyEvent OnEnd;

    Slot enteredSlot;
    public void OnEnable()
    {
        if (DataManager.GetData().events[QuestDatabase.GetQuestWithTitle(thisEvent).ID] == 2)
            gameObject.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Inventory.instance.selectedSlot?.GetItemID() == ItemDatabase.GetItemWithName(neededItem).ID)
        {
            enteredSlot = Inventory.instance.selectedSlot;
            Inventory.instance.interactMethod = () => OnEnd.Invoke();//CallbackFunction(temp);
        }
        else
            Inventory.instance.interactMethod = () => OnFailed.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Inventory.instance.interactMethod = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick.Invoke();
    }

    public void DeleteEnteredItem()
    {
        DataManager.GetData().items[enteredSlot.GetItemID()] = 2;
        Destroy(enteredSlot.gameObject);
    }

    
}

