using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IntroStart : MonoBehaviour
{
    public string nextSceneName;
    private void Start()
    {
        DataManager.Save();
        ChatSystem2.instance.StartChat("I", 1, () =>
         PlayUIManager.instance.FadeOutForNextScene(nextSceneName));
     }
}
