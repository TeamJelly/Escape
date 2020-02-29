using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_Lantern : Puzzle
{
    public GameObject firstPanel;
    public Button lid;

    public GameObject secondPanel;
    public Interactor batteryInteractor;

    public GameObject thirdPanel;
    public Interactor lidInteractor;

    public GameObject fourthPanel;
    bool complete = false;
    bool usedBattery = false;

    public override void InitPuzzle()
    {
        OnEnd.AddListener(() => { OnDisable.Invoke(); });
        OnDisable.AddListener(() => 
        {
            complete = true;
            firstPanel.SetActive(false);
            secondPanel.SetActive(false);
            thirdPanel.SetActive(false);
            fourthPanel.SetActive(true);

        });
        OnEnable.AddListener(() =>
        {
            if (!complete)
            {
                firstPanel.SetActive(true);
                secondPanel.SetActive(false);
                thirdPanel.SetActive(false);
            }
        });
        /***********************뚜껑따기************************/
        lid.onClick.AddListener(() =>
        {
            firstPanel.SetActive(false);
            secondPanel.SetActive(true);
        });
        /***********************건전지 장착************************/
        batteryInteractor.condition = ()=>
        {
            return Inventory.instance.selectedItem == "건전지";
        };
        batteryInteractor.OnEnd.AddListener(() =>
        {
            usedBattery = true;
            Inventory.instance.SubItem("건전지");
            secondPanel.SetActive(false);
            thirdPanel.SetActive(true);
        });
        /***********************뚜껑덮기************************/
        lidInteractor.condition = () =>
        {
            return Inventory.instance.selectedItem == "손전등 뚜껑";
        };

        lidInteractor.OnClick.AddListener(() =>
        {
            ChatSystem2.instance.Monologue("뚜껑 덮어야지ㅡㅡ");
        });

        lidInteractor.OnFailed.AddListener(() =>
        {
            ChatSystem2.instance.Monologue("아니야");
        });

        lidInteractor.OnEnd.AddListener(() =>
        {
            
            ChatSystem2.instance.Monologue("좋아, 됬다.");
            OnEnd.Invoke();
            DataManager.GetData().puzzles[puzzleID] = 2;
            DataManager.Save();
        });

        
    }
    public new void ExitPuzzle()
    {
        base.ExitPuzzle();
        if (!complete)
        {  
            if(usedBattery)
                Inventory.instance.AddItem("건전지");
            usedBattery = false;
        }
    }
}
