using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IntroStart : MonoBehaviour
{
    public GameObject GameScreen;
    private void Start()
    {
        GameScreen.SetActive(false);
        if (StateDatabase.GetStateValue("[게임 시작]") == 0) //게임 시작이 되지 않았으면 인트로를 다시 봄니다.
            ChatSystem2.instance.StartChat("Intro/Z", () =>
            {
                PuzzleManager.instance.StartPuzzleWithName("베개바꿔치기");
                StateManager.instance.EnableState("[게임 시작]");
                GameScreen.SetActive(true);
                DataManager.Save_Auto();
            });
        else //인트로를 봤으면 베게 바꿔치기부터 합니다.
        {
            GameScreen.SetActive(true);
            PuzzleManager.instance.StartPuzzleWithName("베개바꿔치기");
        }
    }
}
//인트로 ABCD 바뀔때 저장 필요