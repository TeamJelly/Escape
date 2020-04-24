using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bedroom : MonoBehaviour
{
    public GameObject GameScreen;
    public GameObject MoodLight;

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

        if (DataManager.GetStates()["화장실최초입장"] == true || DataManager.GetStates()["다용도실최초입장"] == true)
        {
            MoodLight.SetActive(false);
            //ChatSystem2.instance.Go("");
        }


    }
}
