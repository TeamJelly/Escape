using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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

    public GameObject lid;
    public GameObject battery;
    public GameObject fixedBattery;

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
            puzzle.Item = battery;
            puzzle.InitPuzzle();
        });
        trigger.triggers.Add(entry);

        puzzle.OnComplete.AddListener(() =>
        {
            fixingPanel.SetActive(true);
            fixedBattery.SetActive(true);
            trigger.triggers.Clear();
            puzzle.Item = lid;
            puzzle.InitPuzzle();
            puzzle.OnComplete.RemoveAllListeners();
            puzzle.OnComplete.AddListener(() =>
            {
                CompletePuzzle();
            });
        });
//        button.onClick.AddListener(() => ChatSystem2.instance.Monologue("켜지지 않는다."));

    }
    public new void CompletePuzzle()
    {
        InventoryManager.instance.LoseItem("안작동손전등");
        InventoryManager.instance.LoseItem("건전지");
        InventoryManager.instance.GetItem("작동손전등");
        base.CompletePuzzle();
    }
    
    public void ExitPuzzle()
    {
/*        if (!complete)
        {
            if (usedBattery)
                InventoryManager.instance.GetItemSilent("건전지");
            usedBattery = false;
        }*/
    }
}
