using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IntroStart : MonoBehaviour
{
    public string nextSceneName;

    public GameObject BG1;
    public GameObject BG2;
    public GameObject BG3;
    private void Start()
    {
        //DataManager.Save();

        ChatSystem2.instance.StartChat(1,1);
     }
}
