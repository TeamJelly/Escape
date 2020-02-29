using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_UseLantern : Puzzle
{
    public override void InitPuzzle()
    {
        OnEnd.AddListener(()=>
        {
            OnDisable.Invoke();
            DataManager.GetData().puzzles[puzzleID] = 2;
            DataManager.Save();
        });
    }
}
