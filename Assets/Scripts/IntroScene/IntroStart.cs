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
        if (DataManager.GetStates()["인트로다봄"] == false) //게임 시작이 되지 않았으면 인트로를 다시 봄니다.
            ChatSystem2.instance.StartChat("Intro/Z", () =>
            {
                DataManager.SetState("인트로다봄", true);
                GameScreen.SetActive(true);
            });
        else
            GameScreen.SetActive(true);
    }
}