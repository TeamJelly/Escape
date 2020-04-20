using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class P_Lantern : Puzzle
{
    public Button button;//랜턴 온/오프 스위치.
    public EventTrigger trigger;

    public P_UseItem puzzle;

    public GameObject mainPanel;//퍼즐이 아닌 키고끄는 패널.
    public GameObject fixingPanel;// 진짜 퍼즐.
    public GameObject lightOn;
    public GameObject lightOff;
   

    public GameObject lid;
    public GameObject battery_slot;
    public GameObject fixedBattery;

    bool complete = false;
    bool usedBattery = false;

    public override void InitPuzzle()
    { 
        /***********************뚜껑따기************************/
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => {
            lid.SetActive(true);
            Debug.Log("?");
            mainPanel.SetActive(false);
            fixingPanel.SetActive(true);
            trigger.triggers.Clear();
            puzzle.Trigger = trigger;
            puzzle.Slot = battery_slot;
            puzzle.CompleteMonologue = "전지 장착완료.";
            puzzle.InitPuzzle();
        });
        trigger.triggers.Add(entry);

        puzzle.OnComplete = () =>
        {
            fixingPanel.SetActive(true);
            fixedBattery.SetActive(true);
            usedBattery = true; // 건전지 사용됨.
            trigger.triggers.Clear();
            puzzle.Slot = lid;
            puzzle.CompleteMonologue = "뚜껑 장착완료.";
            puzzle.InitPuzzle();
            puzzle.OnComplete = () => 
            {
                CompletePuzzle();
            };
        };
        button.onClick.AddListener(() => ChatSystem2.instance.Monologue("켜지지 않는다."));
    }
    public new void CompletePuzzle()
    {
        base.CompletePuzzle();
        gameObject.SetActive(true);
        mainPanel.SetActive(true);
        lid.SetActive(false);
        fixingPanel.SetActive(false);
        button.onClick.RemoveAllListeners();
        trigger.triggers.Clear();
        button.onClick.AddListener(() =>
        {
            lightOff.SetActive(!lightOff.activeSelf);
            lightOn.SetActive(!lightOn.activeSelf);
        });

    }

    public void ExitPuzzle()
    {
//        base.ExitPuzzle();
        if (!complete)
        {  
            if(usedBattery)
                Inventory.instance.AddItem("건전지");
            usedBattery = false;
        }
    }
}
