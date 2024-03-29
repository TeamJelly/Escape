﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bedroom : MonoBehaviour
{
    public GameObject GameScreen;

    private void Start()
    {
        if (DataManager.GetStates()["인트로다봄"] == false && DataManager.GetStates()["거실최초입장"] == false) //게임 시작이 되지 않았으면 인트로를 다시 봄니다.
        {
            GameScreen.SetActive(false);
            ChatSystem2.instance.StartChat("Intro/Z", () =>
            {
                DataManager.SetState("인트로다봄", true);
                GameScreen.SetActive(true);
            });
        }

        if (DataManager.GetStates()["화장실최초입장"] == true || DataManager.GetStates()["다용도실최초입장"] == true)
        {
            //Lantern.instance.OtherLight.SetActive(false);
            //ChatSystem2.instance.Go("");
        }
    }
}
