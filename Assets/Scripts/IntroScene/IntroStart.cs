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
        DataManager.Save();


        Action d = () => ChatSystem.instance.StartChat(4, () => PlayUIManager.instance.FadeOutForNextScene(nextSceneName));
        Action c = () => ChatSystem.instance.StartChat(3, () => { BG2.SetActive(false); BG3.SetActive(true); PlayUIManager.instance.FadeIn(d); });
        Action b = () => ChatSystem.instance.StartChat(2, () => { BG1.SetActive(false); BG2.SetActive(true); PlayUIManager.instance.FadeIn(c);});
        Action a = () => ChatSystem.instance.StartChat(1, () => { PlayUIManager.instance.FadeOut(b);  });
        a();
        //CallbackManager.todoListNextScene =
        //() =>
        //    {
        //        DataManager.currentData.currentScene = "ForNewUISystem";
        //        DataManager.Save();
               
        //        GameObject funcs = GameObject.Find("Functions");
        //        ChatSystem chat = funcs.GetComponent<ChatSystem>();
        //        GameObject playUI = UIFunctions.currentUI;
        //        GameObject chatUI = chat.chatUI;
        //        UIFunctions.currentUI = chatUI;//SwabCurrentUI(chatUI);
        //        chat.StartChat(
        //            () =>
        //                {
        //                    funcs.GetComponent<PlayManager>().GetQuest("Main",10);
        //                    UIFunctions.SwabCurrentUI(playUI);
        //                });

        //    };
        }
}
