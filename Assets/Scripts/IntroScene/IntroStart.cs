using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IntroStart : MonoBehaviour
{
    public string nextSceneName;
    public CanvasGroup map;
    private void Start()
    {
        DataManager.Save();
        ChatSystem2.instance.StartChat("Intro","I1",()=> 
        {
            ChatSystem2.instance.StartChat("Intro", "I2", () => 
            {
                PlayUIManager.instance.FadeIn(map);
                QuestManager.instance.AddQuest("[현관 진입]");
            });
        });
       
        
     }
}
