using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_UseLantern : Puzzle
{
    public GameObject blindPanel;
    public override void InitPuzzle()
    {
        blindPanel.SetActive(true);
        OnDisable.AddListener(() => { blindPanel.SetActive(false); });

        OnEnd.AddListener(()=>
        {
            OnDisable.Invoke();
            PuzzleManager.instance.GetPuzzleWithName("손전등").ExitPuzzle();
            Inventory.instance.SubItem("손전등");
            DataManager.GetData().puzzles[puzzleID] = 2;
            DataManager.Save();
        });
    }
}
