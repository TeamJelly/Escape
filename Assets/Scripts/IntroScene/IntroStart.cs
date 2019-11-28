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
                GameObject playUI = GameObject.Find("PlayUI");
                GameObject funcs = GameObject.Find("Functions");
                playUI.SetActive(false);
                funcs.GetComponent<ChatSystem>().StartChat(
                    () =>
                        {
                            funcs.GetComponent<PlayManager>().GetQuest("Main",10);
                            playUI.SetActive(true);
                        });

            };
        }
}
