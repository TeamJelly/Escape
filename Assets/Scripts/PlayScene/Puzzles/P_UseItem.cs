using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class P_UseItem : Puzzle
{
    public Interactor interactor;

    public string usingItem;
    public bool deleteItemOnEnd;
    [Space(20)]
    [TextArea(3,5)]
    public string failedString;
    [TextArea(3, 5)]
    public string successString;
    [TextArea(3, 5)]
    public string clickString;


    public override void InitPuzzle()
    {
        interactor.condition = SuccessCondition;

        OnEnd.AddListener(() => { DisablePuzzle();});


        interactor.OnFailed.AddListener(() =>
        {
            ChatSystem2.instance.Monologue(failedString);
        });

        interactor.OnClick.AddListener(() =>
        {
            ChatSystem2.instance.Monologue(clickString);
        });

        interactor.OnEnd.AddListener(() =>
        {
            ChatSystem2.instance.Monologue(successString);
            if (deleteItemOnEnd) Inventory.instance.SubItem(usingItem);
            PuzzleDatabase.SetPuzzleState(puzzleName,2);
            DataManager.Save_Auto();
            OnEnd.Invoke();
        });
    }
    //퍼즐 성공조건
    public bool SuccessCondition()
    {
        return Inventory.instance.selectedItem == usingItem;
    }
}
