using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class P_Entrance : Puzzle
{
    public CanvasGroup DigitalLock;
    public CanvasGroup AnalogLock;

    public EventTrigger DigitalLockTrigger;
    public EventTrigger AnalogLockTrigger;

    public override void InitPuzzle()
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => {
            if (AnalogLock.gameObject.activeSelf)
                PlayUIManager.instance.FadeOut(AnalogLock, () => { PlayUIManager.instance.FadeIn(DigitalLock); });
            else
                PlayUIManager.instance.FadeIn(DigitalLock);
        });
        DigitalLockTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => {
            if (DigitalLock.gameObject.activeSelf)
                PlayUIManager.instance.FadeOut(DigitalLock, () => { PlayUIManager.instance.FadeIn(AnalogLock); });
            else
                PlayUIManager.instance.FadeIn(AnalogLock);
        });
        AnalogLockTrigger.triggers.Add(entry);
    }
}
