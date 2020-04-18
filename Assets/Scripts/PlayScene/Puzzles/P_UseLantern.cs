using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_UseLantern : Puzzle
{
    public GameObject blindPanel;
    public override void InitPuzzle()
    {
        blindPanel.SetActive(true);
//        OnDisable.AddListener(() => { blindPanel.SetActive(false); });
/*
        OnEnd.AddListener(()=>
        {
            OnDisable.Invoke();
//            PuzzleManager.instance.GetPuzzleWithName("손전등 조립").ExitPuzzle();
            Inventory.instance.SubItem("손전등");
            PuzzleDatabase.SetPuzzleState(PuzzleName, 2);
            DataManager.Save_Auto();
        });*/
    }
}
