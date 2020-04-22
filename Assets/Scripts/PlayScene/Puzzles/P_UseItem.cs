using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class P_UseItem : Puzzle
{
    public GameObject Item;//이것을
    public EventTrigger Trigger;//여기에 넣으면 성공!

    public UnityEvent OnClick;
    public UnityEvent OnComplete;

    public string EnteredItem = "";
    public bool RemoveItemOnComplete = false;

    public override void InitPuzzle()
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drop;
        entry.callback.AddListener((data) => {
            if (Item.name == EnteredItem)
                CompletePuzzle();
        });
        Trigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => {
            EnteredItem = InventoryManager.instance.currentSelectItem;
        });
        Trigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((data) => {
            EnteredItem = "";
        });
        Trigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => {
            ClickPuzzle();
        });
        Trigger.triggers.Add(entry);
    }

    public void ClickPuzzle()
    {
        OnClick?.Invoke();
    }

    public new void CompletePuzzle()
    {      
        if (RemoveItemOnComplete)
            InventoryManager.instance.LoseItem(EnteredItem);

        base.CompletePuzzle();
        OnComplete?.Invoke();
    }
}
