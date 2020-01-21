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
    System.Action func;
};
public class ChatSystem : MonoBehaviour
{

    public GameObject chatUI;
    public Text chatTxt;
    public GameObject[] Charactor;
    GameObject currentCharactor;
    System.Action onEnd = null;

    public Chatter[] chatList;
    int index = 0;
    public static ChatSystem instance;
    Dictionary<string, int> nameConvertor = new Dictionary<string, int>();
    string stackedChat = "";

    class state
    { }

    

    Dictionary<string, System.Action<string>> example = new Dictionary<string, Action<string>>();
    Dictionary<string, GameObject> states = new Dictionary<string, GameObject>();

    GameObject anger;
    GameObject happy;
    public void ChangeState(string state)
    {
        Debug.Log(state);
    }
    public void Awake()
    {
        instance = this;
        nameConvertor.Add("x", -1);
        for (int i = 0; i < Charactor.Length; i++)
        {
            nameConvertor.Add(Charactor[i].name, i);
            Debug.Log(Charactor[i].name);
        }

        states.Add("화남", anger);
        states.Add("기쁨", happy);

        

        example.Add("changeScene", ChangeState);

        example["changeScene"]("Hello");
    }
    public void ChatTest(int chatNum)
    {
        StartChat(chatNum, () => { });
    }

    public void StartChat(int chatNum, System.Action onEnd)
    {
        Interpret("ChatDB/Chat#1", chatNum);
        this.onEnd = onEnd;
        //Time.timeScale = 0;
        index = 0;
        chatTxt.text = "";
        currentCharactor = Charactor[chatList[index].who];
        currentCharactor.SetActive(true);
        chatUI.SetActive(true);
        coroutine = StringAnimation(chatList[index].talkString);
        StartCoroutine(coroutine);
    }

    public void SkipChat()
    {
        StopCoroutine(coroutine);
        chatUI.SetActive(false);
        Charactor[chatList[index].who].SetActive(false);
        chatTxt.text = "";
        //Time.timeScale = 1;
        onEnd();
    }

    IEnumerator coroutine;
    bool isCoroutineRunning = false;
    IEnumerator StringAnimation(string text)
    {
        isCoroutineRunning = true;
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
            chatTxt.text = stackedChat + chatList[index].talkString;
            return;
        }
        index++;
        if (index == chatList.Length)
        {
            chatUI.SetActive(false);
            chatTxt.text = "";
            onEnd();
            return;
        }
        if (chatList[index].who == -1)
        {          
            chatTxt.text += "\n";
            stackedChat = chatTxt.text;
            coroutine = StringAnimation(chatList[index].talkString);
            StartCoroutine(coroutine);
            return;
        }
        else stackedChat = "";
        currentCharactor.SetActive(false);
        
        currentCharactor = Charactor[chatList[index].who];
        currentCharactor.SetActive(true);
        chatTxt.text = "";
        coroutine = StringAnimation(chatList[index].talkString);
        StartCoroutine(coroutine);
    }
    void Interpret(string _strSource,int chatNum)
    {
        List<Chatter> chatList = new List<Chatter>();
        TextAsset textAsset = (TextAsset)Resources.Load(_strSource);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList xmlNodeList = xmlDoc.SelectNodes("Chat/S" + chatNum);

        foreach (XmlNode node in xmlNodeList)
        {
            if (node.HasChildNodes)
            {
                foreach(XmlNode child in node.ChildNodes)
                chatList.Add(new Chatter
                {
                    // who = child.Attributes.GetNamedItem("who").Value,
                    who = nameConvertor[child.Attributes.GetNamedItem("who").Value],
                    talkString = child.Attributes.GetNamedItem("message").Value,
                });

            }

        }
        this.chatList = chatList.ToArray();
    }
}
