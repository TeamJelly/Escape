using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class P_UseItem : Puzzle
{
    public Slot Slot;//AnswerItem
    public EventTrigger Trigger;//GoalObject에 넣으면 성공!

    [TextArea(2, 3)]
    public string CompleteMonologue;

    [TextArea(2, 3)]
    public string ClickMonologue;

    public string EnteredItem = "";

    public override void InitPuzzle()
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drop;
        entry.callback.AddListener((data) => {
            if (Slot.name == EnteredItem)
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
        ChatSystem2.instance.Monologue(ClickMonologue);
    }

    public new void CompletePuzzle()
    {
        ChatSystem2.instance.Monologue(CompleteMonologue);
        InventoryManager.instance.LoseItem(EnteredItem);
        base.CompletePuzzle();
    }
}
