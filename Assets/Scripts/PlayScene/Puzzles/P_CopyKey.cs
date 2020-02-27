using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_CopyKey : Puzzle
{
    public Interactor interactor1;//부러진 열쇠1
    public Interactor interactor2;//부러진 열쇠2
    public Interactor interactor3;//테이프
    public Interactor interactor4;//니퍼

    bool state1 = false; //부러진 열쇠1
    bool state2 = false; //부러진 열쇠2
    bool complete = false;
    public override void InitPuzzle()
    {

        OnEnd.AddListener(() => { DisablePuzzle(); });


        interactor1.condition = Condition1;

        interactor1.OnFailed.AddListener(() =>
        {
            ChatSystem2.instance.Monologue("이게 아니야");
        });

        interactor1.OnClick.AddListener(() =>
        {
            ChatSystem2.instance.Monologue("복제할 열쇠조각이 필요해");
        });

        interactor1.OnEnd.AddListener(() =>
        {
            state1 = true;
            Inventory.instance.SubItem("부러진 열쇠1");
            if (state2) interactor3.gameObject.SetActive(true);
        });
        /**********************************************************/
        interactor2.condition = Condition2;

        interactor2.OnFailed.AddListener(() =>
        {
            ChatSystem2.instance.Monologue("이게 아니야");
        });

        interactor2.OnClick.AddListener(() =>
        {
            ChatSystem2.instance.Monologue("복제할 열쇠조각이 필요해");
        });

        interactor2.OnEnd.AddListener(() =>
        {
            state2 = true;
            Inventory.instance.SubItem("부러진 열쇠2");
            if (state1) interactor3.gameObject.SetActive(true);
        });
        /**********************************************************/
        interactor3.condition = Condition3;

        interactor3.OnFailed.AddListener(() =>
        {
            if(interactor3.enteredSlot.GetItemName() == "니퍼")
                ChatSystem2.instance.Monologue("자르려고하니깐 조각이 움직이네...");
            else ChatSystem2.instance.Monologue("이게 아니야");
        });

        interactor3.OnClick.AddListener(() =>
        {
            ChatSystem2.instance.Monologue("이제 잘라볼까?");
        });

        interactor3.OnEnd.AddListener(() =>
        {
            interactor4.gameObject.SetActive(true);
        });
        /**********************************************************/
        interactor4.condition = Condition4;

        interactor4.OnFailed.AddListener(() =>
        {
            ChatSystem2.instance.Monologue("이게 아니야");
        });

        interactor4.OnClick.AddListener(() =>
        {
            ChatSystem2.instance.Monologue("이제 잘라볼까?");
        });

        interactor4.OnEnd.AddListener(() =>
        {
            ChatSystem2.instance.Monologue("열쇠가 완성되었다.");
            Inventory.instance.SubItem("니퍼");
            Inventory.instance.SubItem("주민등록증");
            Inventory.instance.AddItem("복제된 열쇠");
            complete = true;
            OnEnd.Invoke();
        });
        /**********************************************************/

    }
    public bool Condition1()
    {
        return Inventory.instance.selectedSlot?.GetItemName() == "부러진 열쇠1";
    }

    public bool Condition2()
    {
        return Inventory.instance.selectedSlot?.GetItemName() == "부러진 열쇠2";
    }

    public bool Condition3()
    {
        return Inventory.instance.selectedSlot?.GetItemName() == "테이프";
    }

    public bool Condition4()
    {
        return Inventory.instance.selectedSlot?.GetItemName() == "니퍼";
    }

    public new void ExitPuzzle()
    {
        base.ExitPuzzle();
        if (!complete)
        {
            interactor3.gameObject.SetActive(false);
            interactor4.gameObject.SetActive(false);
            interactor1.gameObject.SetActive(true);
            interactor2.gameObject.SetActive(true);
            state1 = false;
            state2 = false;
            Inventory.instance.AddItem("부러진 열쇠1");
            Inventory.instance.AddItem("부러진 열쇠2");
        }
    }

}
