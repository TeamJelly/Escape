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

    public List<Charactor> charactorList;// = new List<Charactor>();
    public GameObject thisUI;
    public Transform charactorPanel;
    public Image bgPanel;
    public Text chatText;
    public Button skipButton;

    Dictionary<string, Charactor> charactorFinder = new Dictionary<string, Charactor>();
    List<MessageBox> messageList = new List<MessageBox>();
    List<int> skipPoint = new List<int>();
    int skipCount = 0;

    Image currentCharactor;
    Color listeningColor = new Color(50 / 255f, 50 / 255f, 50 / 255f);
    Color tellingColor = new Color(1, 1, 1);

    int currentIndex = 0;
    IEnumerator typeCoroutine;
    bool isTypeCoroutineRunning = false;
    string stackedChat = "";
    Action onEnd = null;

    public static ChatSystem2 instance;

    private void Awake()
    {
        instance = this;
        foreach (Charactor c in charactorList)
        {
            charactorFinder[c.charactorName] = c;
        }
    }

    public void StartChat(int episodeNum,Action endFunc)
    {
        onEnd = endFunc;
        currentIndex = 0;
        thisUI.SetActive(true);
        TextAsset textAsset = (TextAsset)Resources.Load("ChatDB/S" + episodeNum);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNode chatList = xmlDoc.SelectSingleNode("Chat");

        foreach(XmlNode node in chatList.ChildNodes)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                MessageBox box =
                     new MessageBox
                     {
                         name = child.Attributes.GetNamedItem("Who").Value,
                         state = child.Attributes.GetNamedItem("State").Value,
                         message = child.Attributes.GetNamedItem("Message").Value
                     };
                messageList.Add(box);
                if(box.state == "Reset")
                 skipPoint.Add(messageList.Count - 1);
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
        if (currentIndex >= messageList.Count)
        {
            endChat();
            return;
        }

        MessageBox messageBox = messageList[currentIndex];
        Debug.Log(messageBox.name + "," + messageBox.state + "," + messageBox.message);
        //이름이 - 면 텍스트 다음줄에서 계속.
        if (messageBox.name == "-")
        {
            chatText.text += "\n";
            stackedChat = chatText.text + messageBox.message;
            typeCoroutine = TypingAnimation(messageBox.message);
            currentIndex++;
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
        isTypeCoroutineRunning = false;
        if (skipCount < skipPoint.Count)
        {
            chatText.text = "";
            currentIndex = skipPoint[skipCount];
            //skipCount++;
            ShowNext();
        }
        else endChat();
        
        //Time.timeScale = 1;
    }
    public void endChat()
    {
        StopCoroutine(typeCoroutine);
        isTypeCoroutineRunning = false;
        thisUI.SetActive(false);
        if(currentCharactor != null)
            currentCharactor.gameObject.SetActive(false);
        chatText.text = "";
        onEnd();

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
        currentIndex++;
        ShowNext();
    }

    public void StopBGM()
    {
        bgmAudio.Pause();
        currentIndex++;
        ShowNext();
    }

    //효과음
    public void StartSE(string name)
    {
        seAudio.clip = (AudioClip)Resources.Load("SE/" + name);
        Debug.Log("SE start");
        seAudio.Play();
        currentIndex++;
        ShowNext();
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
            StartCoroutine(FadeIn(t.GetComponent<CanvasGroup>(), ()=> { }));
        }
        currentIndex++;
        ShowNext();
    }
    //인물 cg재배치
    public void GoSCG(string charactors)
    {
        string[] result = charactors.Split(new string[] { ", " }, StringSplitOptions.None);
        foreach (string charactor in result)
        {
            Transform t = charactorFinder[charactor].transform;
            t.SetAsLastSibling();
            StartCoroutine(FadeIn(t.GetComponent<CanvasGroup>(), () => { }));
        }
        currentIndex++;
        ShowNext();
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
        currentIndex++;
        ShowNext();
    }
    //모든 인물 cg숨기기
    public void HideAllSCG()
    {
        foreach(Charactor c in charactorList)
        {
            StartCoroutine(FadeOut(c.gameObject.GetComponent<CanvasGroup>(), () =>
            {
                c.transform.SetParent(thisUI.transform);
                c.gameObject.SetActive(false);
            }));
        }
        currentIndex++;
        ShowNext();
    }
    //배경 CG를 보여줍니다.
    public void ShowBGCG(string name)
    {
        StartCoroutine(FadeOut(bgPanel.GetComponent<CanvasGroup>(), () =>
        {
            chatText.text = "";
            bgPanel.sprite = Resources.Load("BGCG/" + name, typeof(Sprite)) as Sprite;
            StartCoroutine(FadeIn(bgPanel.GetComponent<CanvasGroup>(), () =>
            {
                currentIndex++;
                ShowNext();
            }));
        }));
    }
    //잠들어서 까망화면
    public void HideBGCG()
    {
        StartCoroutine(FadeOut(bgPanel.GetComponent<CanvasGroup>(), () =>
        {
            currentIndex++;
            ShowNext();
        }));
    }
    IEnumerator FadeIn(CanvasGroup fadeObject, System.Action onEnd)
    {
        float tempAlpha = 0;
        float fadeTime = 0.2f;
        while (tempAlpha < 1f)
        {
            fadeObject.alpha = tempAlpha;
            tempAlpha += Time.deltaTime / fadeTime;
            yield return null;
        }
        fadeObject.alpha = 1.0f;
        onEnd();
    }
    IEnumerator FadeOut(CanvasGroup fadeObject, System.Action onEnd)
    {
        float tempAlpha = fadeObject.alpha;
        float fadeTime = 0.2f;
        while (tempAlpha > 0)
        {
            fadeObject.alpha = tempAlpha;
            tempAlpha -= Time.deltaTime / fadeTime;
            yield return null;
        }
        fadeObject.alpha = 0;
        onEnd();
    }
    public void PopUpImage(string name)
    {
        //팝업이미지띄움
        currentIndex++;
        ShowNext();
    }
    public void PopDownImage(string name)
    {
        currentIndex++;
        ShowNext();
    }
    public void Reset()
    {
        HideAllSCG();
        //HideBGCG();
        skipCount++;
    }
}
