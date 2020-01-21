using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using UnityEngine.UI;
using System.Reflection;
using System;
public class ChatSystem2 : MonoBehaviour
{
    class MessageBox
    {
        public string name;
        public string state;
        public string message;
    }

    public List<Charactor> charactorList = new List<Charactor>();
    public GameObject thisUI;
    public Transform charactorPanel;
    public Image bgPanel;
    
    public Text chatText;

    Dictionary<string, Charactor> charactorFinder = new Dictionary<string, Charactor>();

    XmlNodeList xmlNodeList;
    List<MessageBox> messageList = new List<MessageBox>();

    Image currentCharactor;
    int currentIndex = 0;
    IEnumerator typeCoroutine;
    bool isTypeCoroutineRunning = false;
    string stackedChat = "";
    Color listeningColor = new Color(50/255f, 50 / 255f, 50 / 255f);
    Color tellingColor = new Color(1, 1, 1);
    public static ChatSystem2 instance;

    private void Awake()
    {
        instance = this;
        foreach (Charactor c in charactorList)
        {
            charactorFinder[c.charactorName] = c;
        }
    }

    public void StartChat(int episodeNum, int chatNum)
    {
        currentIndex = 0;
        thisUI.SetActive(true);
        TextAsset textAsset = (TextAsset)Resources.Load("ChatDB/S" + episodeNum);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        xmlNodeList = xmlDoc.SelectNodes("Chat/S" + episodeNum + "-" + chatNum);
        foreach (XmlNode node in xmlNodeList)
        {
            if (node.HasChildNodes)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    messageList.Add(
                        new MessageBox
                        {
                            name = child.Attributes.GetNamedItem("Who").Value,
                            state = child.Attributes.GetNamedItem("State").Value,
                            message = child.Attributes.GetNamedItem("Message").Value
                        });
                }
            }
        }
        ShowNext();
    }

    IEnumerator TypingAnimation(string text)
    {
        isTypeCoroutineRunning = true;
        for (int i = 0; i < text.Length; i++)
        {
            chatText.text += text[i];
            yield return new WaitForSecondsRealtime(0.05f);
        }
        isTypeCoroutineRunning = false;
    }

    public void ShowNext()
    {
        //현재 쳐지고 있는 텍스트 있으면 완성시키고 종료
        if (isTypeCoroutineRunning)
        {
            StopCoroutine(typeCoroutine);
            isTypeCoroutineRunning = false;
            chatText.text = stackedChat;
            stackedChat = "";
            //currentIndex++;
            return;
        }
        
        //모든 대사 보여줬으면 비활성화
        if (currentIndex == messageList.Count)
        {
            thisUI.SetActive(false);
            chatText.text = "";
            return;
        }

        MessageBox messageBox = messageList[currentIndex];

        //이름이 - 면 텍스트 다음줄에서 계속.
        if (messageBox.name == "-")
        {
            chatText.text += "\n";
            stackedChat = chatText.text + messageBox.message;
            typeCoroutine = TypingAnimation(messageBox.message);
            StartCoroutine(typeCoroutine);
            return;
        }

        //이름이 System이면 명령 호출
        if (messageBox.name == "System")
        {
            if (messageBox.message == "-")
                GetCommand(messageBox.state);
            else 
                GetCommand(messageBox.state, messageBox.message);
            currentIndex++;
            ShowNext();
            return;
        }

        else
        {
            chatText.text = "";
            if (charactorFinder.ContainsKey(messageBox.name))
            {
                //표정변경
                charactorFinder[messageBox.name].SetEmotion(messageBox.state);

                //말하고 있는 인물 부각효과
                
                if (currentCharactor != null)
                {
                    currentCharactor.color = listeningColor;
                }

                currentCharactor = charactorFinder[messageBox.name].GetComponent<Image>();
                if (currentCharactor != null)
                    currentCharactor.color = tellingColor;
            }

            //타이핑 시작
            stackedChat = messageBox.message;
            typeCoroutine = TypingAnimation(messageBox.message);
            StartCoroutine(typeCoroutine);
        }
        currentIndex++;
    }
    public void SkipChat()
    {
        StopCoroutine(typeCoroutine);
        thisUI.SetActive(false);
        currentCharactor.gameObject.SetActive(false);
        chatText.text = "";
        //Time.timeScale = 1;
    }

/***********************************System 명령******************************************************/
    public AudioSource bgmAudio;
    public AudioSource seAudio;
    public void GetCommand(string func, string parameter)
    {
        Type t = GetType();
        MethodInfo method = t.GetMethod(func);
        if (method != null)
            method.Invoke(this, new object[] { parameter });
    }
    public void GetCommand(string func)
    {
        Type t = GetType();
        MethodInfo method = t.GetMethod(func);
        if (method != null)
            method.Invoke(this, null);
    }
    public void StartBGM(string name)
    {
        bgmAudio.clip = (AudioClip)Resources.Load("BGM/" + name);
        bgmAudio.loop = true;
        bgmAudio.Play();

    }

    public void StopBGM()
    {
        bgmAudio.Pause();
    }

    //효과음
    public void StartSE(string name)
    {
        seAudio.clip = (AudioClip)Resources.Load("SE/" + name);
        seAudio.Play();
    }


    //인물 cg배치
    public void ShowSCG(string charactors)
    {
        string[] result = charactors.Split(new string[] { ", " }, StringSplitOptions.None);
        foreach (string charactor in result)
        {
            
            Transform t = charactorFinder[charactor].transform;
            charactorFinder[charactor].GetComponent<Image>().color = listeningColor;
            t.SetParent(charactorPanel);
            t.SetAsLastSibling();
            t.gameObject.SetActive(true);
        }
    }
    //인물 cg재배치
    public void GoSCG(string charactors)
    {
        string[] result = charactors.Split(new string[] { ", " }, StringSplitOptions.None);
        foreach (string charactor in result)
        {
            Transform t = charactorFinder[charactor].transform;
            t.SetAsLastSibling();
        }
    }
    //인물 cg숨기기
    public void HideSCG(string charactors)
    {
        string[] result = charactors.Split(new string[] { ", " }, StringSplitOptions.None);
        foreach (string charactor in result)
        {
            Transform t = charactorFinder[charactor].transform;
            t.SetParent(thisUI.transform);
            t.gameObject.SetActive(false);
        }
    }
    //모든 인물 cg숨기기
    public void HideAllSCG()
    {
        foreach(Charactor c in charactorList)
        {
            c.transform.SetParent(thisUI.transform);
            c.gameObject.SetActive(false);
        }
    }
    //배경 CG를 보여줍니다.
    public void ShowBGCG(string name)
    {
        bgPanel.sprite = Resources.Load("BGCG/" + name,typeof(Sprite)) as Sprite;
    }
    //잠들어서 까망화면
    public void HideBGCG()
    {

    }
    public void PopUpImage()
    {

    }
}
