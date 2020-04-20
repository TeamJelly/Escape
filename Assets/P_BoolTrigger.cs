using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class P_BoolTrigger : Puzzle
{

    public string StateName;
    public UnityEvent falseTrigger;
    public UnityEvent trueTrigger;

    public new void Awake()
    {
        InitPuzzle();
    }

    public override void InitPuzzle()
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => {
            if (DataManager.GetData().states[StateName] == false)
                falseTrigger.Invoke();
            else if (DataManager.GetData().states[StateName] == true)
                trueTrigger.Invoke();
        });
        GetComponent<EventTrigger>().triggers.Add(entry);
    }

}
