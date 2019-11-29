using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroStart : MonoBehaviour
{
    public ChatSystem chatsystem;
    public string nextSceneName;
    private void Awake()
    {
        DataManager.currentData.currentScene = "Intro";
        DataManager.Save();
        chatsystem.StartChat(() =>  GetComponent<UIFunctions>().SelectScene(nextSceneName));


        CallbackManager.todoListNextScene =
        () =>
            {
                DataManager.currentData.currentScene = "panorama3";
                DataManager.Save();
               
                GameObject funcs = GameObject.Find("Functions");
                UIFunctions uiFunc = funcs.GetComponent<UIFunctions>();
                ChatSystem chat = funcs.GetComponent<ChatSystem>();
                GameObject playUI = uiFunc.currentUI;
                GameObject chatUI = chat.chatUI;
                uiFunc.SwabCurrentUI(chatUI);
                chat.StartChat(
                    () =>
                        {
                            funcs.GetComponent<PlayManager>().GetQuest("Main",10);
                            uiFunc.SwabCurrentUI(playUI);
                        });

            };
        }
}
