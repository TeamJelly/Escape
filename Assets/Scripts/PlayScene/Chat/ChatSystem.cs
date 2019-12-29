using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ChatSystem : MonoBehaviour
{

    [System.Serializable]
    public class chatter
    {
        public int who;
        public string talkString;

    };
    public GameObject chatUI;
    public Text chatTxt;
    public GameObject[] Chatter;
    System.Action onEnd;

    public chatter[] chatList;

    public Button nextButton;
    int index = 0;

    public void StartChat(System.Action onEnd)
    {
        this.onEnd = onEnd;
        Time.timeScale = 0;
        chatTxt.text = chatList[index].talkString;
        Chatter[chatList[index].who].SetActive(true);
        chatUI.SetActive(true);
    }

    public void SkipChat()
    {
        chatUI.SetActive(false);
        Time.timeScale = 1;
        onEnd();
    }

    public void NextString()
    {
        Chatter[chatList[index].who].SetActive(false);
        index++;
        if (index == chatList.Length)
        {
            SkipChat();
            return;
        }
        chatTxt.text = chatList[index].talkString;
        Chatter[chatList[index].who].SetActive(true);

    }
}
