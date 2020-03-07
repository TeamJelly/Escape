using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IntroStart : MonoBehaviour
{
    public string nextSceneName;
    public GameObject map;
    private void Start()
    {

        map.SetActive(false);
        if (QuestDatabase.GetQusetState("[현관 진입]") < 1)
            ChatSystem2.instance.StartChat("Intro", "I1", () =>
              {
                  DataManager.Save_Auto();
                  ChatSystem2.instance.StartChat("Intro", "I2", () =>
                  {
                      PuzzleManager.instance.StartPuzzleWithName("베개바꿔치기");
                      map.SetActive(true);
                      // PlayUIManager.instance.FadeIn(map);
                      DataManager.Save_Auto();
                     
                  });
              });
        else
        {
            map.SetActive(true);
            PuzzleManager.instance.StartPuzzleWithName("베개바꿔치기");
        }
    }
}
