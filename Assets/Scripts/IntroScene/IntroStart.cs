using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IntroStart : MonoBehaviour
{
    public GameObject map;
    private void Start()
    {
        map.SetActive(false);
        //ChatSystem2.instance.StartChat("Intro/A", ()=> { });
        if (StateDatabase.GetStateValue("[게임 시작]") == 0) //게임 시작이 되지 않았으면 인트로를 다시 봄니다.
            ChatSystem2.instance.StartChat("Intro/A", () =>
            {
                 PuzzleManager.instance.StartPuzzleWithName("베개바꿔치기");
                 StateManager.instance.EnableState("[게임 시작]");
                 map.SetActive(true);
                 DataManager.Save_Auto();
            });
        else //인트로를 봤으면 베게 바꿔치기부터 합니다.
        {
            map.SetActive(true);
            PuzzleManager.instance.StartPuzzleWithName("베개바꿔치기");
        }
    }
}
//인트로 ABCD 바뀔때 저장 필요