using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroStart : MonoBehaviour
{
    public string nextSceneName;
    private void Start()
    {
        DataManager.Save();
        ChatSystem.instance.StartChat(1,() => { PlayUIManager.instance.FadeOutForNextScene(nextSceneName);});


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
