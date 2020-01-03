using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]
public class Chatter
{
    public int who;
    public string talkString;

};
public class ChatSystem : MonoBehaviour
{

    public GameObject chatUI;
    public Text chatTxt;
    public GameObject[] Chatter;
    System.Action onEnd = null;

    Chatter[] chatList;
    int index = 0;
    public static ChatSystem instance;
    Dictionary<string, int> nameConvertor = new Dictionary<string, int>();
    public void Awake()
    {
        instance = this;
        nameConvertor.Add("나레이터", 0);
        nameConvertor.Add("주인공", 1);
        nameConvertor.Add("연이", 2);
    }
    public void ChatTest(int chatNum)
    {
        StartChat(chatNum, () => { });
    }

    public void StartChat(int chatNum, System.Action onEnd)
    {
        Interpret("ChatDB/Chat#" + chatNum);
        this.onEnd = onEnd;
        //Time.timeScale = 0;
        index = 0;
             
        Chatter[chatList[index].who].SetActive(true);
        chatUI.SetActive(true);
        coroutine = StringAnimation(chatList[index].talkString);
        StartCoroutine(coroutine);
    }

    public void SkipChat()
    {
        StopCoroutine(coroutine);
        chatUI.SetActive(false);
        Chatter[chatList[index].who].SetActive(false);
        //Time.timeScale = 1;
        onEnd();
    }

    IEnumerator coroutine;
    bool isCoroutineRunning = false;
    IEnumerator StringAnimation(string text)
    {
        isCoroutineRunning = true;
        chatTxt.text = "";
        for(int i = 0; i < text.Length; i++)
        {
            chatTxt.text += text[i];
            yield return new WaitForSecondsRealtime(0.05f);
        }
        isCoroutineRunning = false;
    }
    public void NextString()
    {
        if (isCoroutineRunning)
        {
            StopCoroutine(coroutine);
            isCoroutineRunning = false;
            chatTxt.text = chatList[index].talkString;
            return;
        }
        Chatter[chatList[index].who].SetActive(false);
        index++;
        if (index == chatList.Length)
        {
            chatUI.SetActive(false);
            onEnd();
            return;
        }     
        Chatter[chatList[index].who].SetActive(true);
        coroutine = StringAnimation(chatList[index].talkString);
        StartCoroutine(coroutine);
    }
    void Interpret(string _strSource)
    {
        List<Chatter> chatList = new List<Chatter>();
        TextAsset textAsset = (TextAsset)Resources.Load(_strSource);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList xmlNodeList = xmlDoc.SelectNodes("Chat");
        foreach (XmlNode node in xmlNodeList)
        {
            if (node.Name.Equals("Chat") && node.HasChildNodes)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    chatList.Add(new Chatter
                    {
                        // who = child.Attributes.GetNamedItem("who").Value,
                        who = nameConvertor[child.Attributes.GetNamedItem("who").Value],
                        talkString = child.Attributes.GetNamedItem("message").Value,
                    });
                }

            }

        }
        this.chatList = chatList.ToArray();
    }
}
