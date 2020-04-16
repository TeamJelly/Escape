using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public delegate bool Condition();
//상호작용시켜주는 클래스. 보통은 퍼즐 클래스에서 관리된다.
public class Interactor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    [NonSerialized]
    public UnityEvent OnFailed = new UnityEvent();
    [NonSerialized]
    public UnityEvent OnClick = new UnityEvent();
    [NonSerialized]
    public UnityEvent OnEnd = new UnityEvent();
    public string enteredItem;

    
    public Condition condition;

    public void OnPointerEnter(PointerEventData eventData)
    {
        enteredItem = Inventory.instance.selectedItem;
        if (condition())
        {
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
    
}
