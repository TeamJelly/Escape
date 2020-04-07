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
    public CanvasGroup thisUI;
    public Transform charactorPanel;
    public GameObject bgPanel;
    public Image bgImage;
    public Text chatText;
    public Text charactorName;
    public Button nextButton;
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

    public void Monologue(string[] message)
    {
        chatText.text = "";
        messageList.Clear();
        onEnd = () => bgImage.gameObject.SetActive(true);
        skipButton.onClick.AddListener(EndChat);
        currentIndex = -1;
        foreach(string s in message)
        messageList.Add(
            new MessageBox
            {
                name = "주인공",
                state = "-",
                message = s
            });
        bgImage.gameObject.SetActive(false);
        thisUI.gameObject.SetActive(true);
        
        StartCoroutine(PlayUIManager.instance.AscendAlpha(thisUI, () =>
        {
            ShowNext();
            nextButton.onClick.AddListener(ShowNext);
        }));
    }
    public void Monologue(string message)
    {
        chatText.text = "";
        messageList.Clear();
        onEnd = () => bgImage.gameObject.SetActive(true);
        skipButton.onClick.AddListener(EndChat);
        currentIndex = -1;
        messageList.Add(
            new MessageBox
            {
                name = "주인공",
                state = "-",
                message = message
            });
        bgImage.gameObject.SetActive(false);
        thisUI.gameObject.SetActive(true);
        StartCoroutine(PlayUIManager.instance.AscendAlpha(thisUI, () =>
        {
            ShowNext();
            nextButton.onClick.AddListener(ShowNext);
        }));
    }

    public void StartChat(string link, Action endFunc)
    {
        chatText.text = "";
        onEnd = endFunc;
        currentIndex = -1;
        messageList.Clear();
        
        TextAsset textAsset = (TextAsset)Resources.Load("ChatDB/" + link.Split('/')[0]);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNode chatList = xmlDoc.SelectSingleNode("Chat/" + link.Split('/')[1]);

        foreach (XmlNode node in chatList.ChildNodes)
        {
            MessageBox box =
                     new MessageBox
                     {
                         name = node.Attributes.GetNamedItem("Who") == null ? "-" : node.Attributes.GetNamedItem("Who").Value,
                         state = node.Attributes.GetNamedItem("State") == null ? "-" : node.Attributes.GetNamedItem("State").Value,
                         message = node.Attributes.GetNamedItem("Message") == null ? "-" : node.Attributes.GetNamedItem("Message").Value
                     };
            messageList.Add(box);
            if (box.state == "Reset")
            {
                skipPoint.Add(messageList.Count - 1);
                Debug.Log(messageList.Count - 1);
            }
        }
        Debug.Log("----chatStart----");
        Debug.Log("SkipCount:" + skipCount);
        InventoryUI.instance.DisableInventoryBar();
        StartCoroutine(PlayUIManager.instance.AscendAlpha(thisUI, () =>
        {
            bgPanel.gameObject.SetActive(true);
            thisUI.gameObject.SetActive(true);
            ShowNext();
            skipButton.onClick.AddListener(SkipChat);
            nextButton.onClick.AddListener(ShowNext);
        }));
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
        currentIndex++;
        //현재 쳐지고 있는 텍스트 있으면 완성시키고 종료
        if (isTypeCoroutineRunning)
        {
            StopCoroutine(typeCoroutine);
            isTypeCoroutineRunning = false;
            chatText.text = stackedChat;
            stackedChat = "";
            currentIndex--;
            return;
        }
        
        //모든 대사 보여줬으면 비활성화
        if (currentIndex >= messageList.Count)
        {
            if(currentIndex == messageList.Count) // 마무리 딱 한번만.
            EndChat();
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
            charactorName.text = messageBox.name;
            typeCoroutine = TypingAnimation(messageBox.message);
            StartCoroutine(typeCoroutine);
        }
    }
    public void SkipChat()
    {
        StopCoroutine(typeCoroutine);
        isTypeCoroutineRunning = false;
        Debug.Log("Skip");
        if (skipCount < skipPoint.Count)
        {
            chatText.text = "";
            currentIndex = skipPoint[skipCount] - 1;
            Debug.Log(currentIndex);
            //skipCount++;
            
            ShowNext(); 
        }
        //else EndChat();
        
        //Time.timeScale = 1;
    }
    public void EndChat()
    {
        skipPoint.Clear();
        skipCount = 0;
        skipButton.onClick.RemoveAllListeners();
        nextButton.onClick.RemoveAllListeners();
        
        StartCoroutine(FadeOut(thisUI, () =>
        {
             StopCoroutine(typeCoroutine);
             isTypeCoroutineRunning = false;
             thisUI.gameObject.SetActive(false);
             if (currentCharactor != null)
                 currentCharactor.gameObject.SetActive(false);
             chatText.text = "";
            InventoryUI.instance.EnableInventoryBar();
            bgImage.GetComponent<CanvasGroup>().alpha = 0;
             onEnd?.Invoke();
         }));
        
        //Time.timeScale = 1;
    }
    /***********************************System 명령************************************/
    public AudioSource bgmAudio;
    public AudioSource seAudio;

    public void GetCommand(string func, string parameter)
    {
        Type t = GetType();
        MethodInfo method = t.GetMethod(func); // 함수를 저장
        if (method != null) //이 코드 안에 있는 System함수라면
        {
            method.Invoke(this, new object[] { parameter });
        }
        else //미구현 생략
        {
            Debug.LogError("미구현된 System 함수 : " + func + "{" + parameter + ")");
            ShowNext();
        }
    }
    public void GetCommand(string func)
    {
        Type t = GetType();
        MethodInfo method = t.GetMethod(func); // 함수를 저장
        if (method != null) //이 코드 안에 있는 System함수라면
        { 
            method.Invoke(this, null);
        }
        else //미구현 생략
        {
            Debug.LogError("미구현된 System 함수 : " + func + "()");
            ShowNext();
        }
    }

    public void Go(string link)
    {
        chatText.text = "";
        currentIndex = -1;
        messageList.Clear();

        TextAsset textAsset = (TextAsset)Resources.Load("ChatDB/" + link.Split('/')[0]);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNode chatList = xmlDoc.SelectSingleNode("Chat/" + link.Split('/')[1]);

        foreach (XmlNode node in chatList.ChildNodes)
        {
            MessageBox box =
                     new MessageBox
                     {
                         name = node.Attributes.GetNamedItem("Who") == null ? "-" : node.Attributes.GetNamedItem("Who").Value,
                         state = node.Attributes.GetNamedItem("State") == null ? "-" : node.Attributes.GetNamedItem("State").Value,
                         message = node.Attributes.GetNamedItem("Message") == null ? "-" : node.Attributes.GetNamedItem("Message").Value
                     };
            messageList.Add(box);
            if (box.state == "Reset")
            {
                skipPoint.Add(messageList.Count - 1);
                Debug.Log(messageList.Count - 1);
            }
        }
        Debug.Log("----chatStart----");
        Debug.Log("SkipCount:" + skipCount);
        InventoryUI.instance.DisableInventoryBar();
    }

    public void MakeSelection(string text)
    {
        string [] SelectText = text.Split('/');
        SelectionUIManager.instance.MakeButton("text");
    }

    public void StartBGM(string name)
    {
        bgmAudio.clip = (AudioClip)Resources.Load("BGM/" + name);
        bgmAudio.loop = true;
        bgmAudio.Play();
        ShowNext();
    }

    public void StopBGM()
    {
        bgmAudio.Pause();
        
        ShowNext();
    }

    //효과음
    public void StartSE(string name)
    {
        seAudio.clip = (AudioClip)Resources.Load("SE/" + name);
        Debug.Log("SE start");
        seAudio.Play();
        
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
        
        ShowNext();
    }
    //배경 CG를 보여줍니다.
    public void ShowBGCG(string name)
    {
        StartCoroutine(FadeOut(bgImage.GetComponent<CanvasGroup>(), () =>
        {
            chatText.text = "";
            bgImage.sprite = Resources.Load("BGCG/" + name, typeof(Sprite)) as Sprite;
            StartCoroutine(FadeIn(bgImage.GetComponent<CanvasGroup>(), () =>
            {
                
                ShowNext();
            }));
        }));
    }
    //잠들어서 까망화면
    public void HideBGCG()
    {
        StartCoroutine(FadeOut(bgImage.GetComponent<CanvasGroup>(), () =>
        {
            
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
       // currentIndex++;
        ShowNext();
    }
    public void PopDownImage(string name)
    {
        ShowNext();
    }

    /*public void GetQuest(string name)
    {
        QuestManager.instance.GetQuest(name);
        ShowNext();
    }
    public void FinishQuest(string name)
    {
        QuestManager.instance.FinishQuest(name);
        ShowNext();
    }*/
    public void GetItem(string name)
    {
        Inventory.instance.GetItem(name);
        ShowNext();
    }

    public void StartPuzzle(string name)
    {
        PuzzleManager.instance.StartPuzzleWithName(name);
        ShowNext();
    }

    public void Reset()
    {
        skipCount++;
        HideAllSCG();
        //HideBGCG();
    }
}
