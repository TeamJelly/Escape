using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

//말풍선 관리해주는 클래스
public class SpeechBaloonManager : MonoBehaviour
{

    public Button[] baloons;
    public Button nextButton;
    public Button prevButton;
    int currentIndex = 0;

    //현재 활성화 되어있는 상호작용 리스트 activatedList
   // public List<int> activatedList = new List<int>();
    public List<string> activatedList = new List<string>();

    static Dictionary<string, int> titleToID = new Dictionary<string, int>();
    //System.Action[] actions;

    public static SpeechBaloonManager instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        nextButton.gameObject.SetActive(false);
        prevButton.gameObject.SetActive(false);

        //현재 data에서 for문 돌려서 활성화 되어있는 Dialog들을 activatedList에 추가
        InitActivatedList();
       
        //다음버튼 클릭시 동작 지정
        nextButton.onClick.AddListener(() =>
        {
            prevButton.gameObject.SetActive(true);
            currentIndex += baloons.Length;
            SetBaloons(currentIndex);
            //더이상 다음페이지 보여줄게 없으면 버튼 비활성화
            if (currentIndex + baloons.Length >= activatedList.Count)
            {
                nextButton.gameObject.SetActive(false);
            }
        });

        //이전버튼 클릭시 동작 지정
        prevButton.onClick.AddListener(() =>
        {
            nextButton.gameObject.SetActive(true);
            currentIndex -= baloons.Length;
            SetBaloons(currentIndex);
            //더이상 이전페이지 보여줄게 없으면 버튼 비활성화
            if (currentIndex - baloons.Length < 0)
            {
                prevButton.gameObject.SetActive(false);
            }
        });

    }

    void InitActivatedList()
    {
        PlayerData data = DataManager.GetData();

        //해당구문은 테스트 목적
        //data.dialogs[0] = 1;
        //data.dialogs[1] = 1;
        //data.dialogs[2] = 1;
        
        for (int i = 0; i < data.dialogs.Length; i++)
        {
            if (data.dialogs[i] == 1)
            {
                //activatedList.Add(i);
                string title = GetTitleWithID(i);
                activatedList.Add(title);
            }
        }
        //만약 말풍선개수보다 상호작용개수가 많다면 다음 버튼 활성화
        if (activatedList.Count > baloons.Length)
            nextButton.gameObject.SetActive(true);

        //첫 페이지 리스트 뿌려주기.
        SetBaloons(0);
    }

    public void SubBaloon(int index)
    {
        activatedList.RemoveAt(index);
    }
    public void AddBaloon(int id)
    {
        DataManager.GetData().dialogs[id] = 1;
        string title = GetTitleWithID(id);
        if (activatedList.Contains(title)) return;
        activatedList.Add(title);
        SetBaloons(currentIndex);
    }
    void SetBaloons(int index)
    {
        for (int i = 0; i < baloons.Length; i++)
        {
            int thisIndex = i + index;
            if (thisIndex < activatedList.Count)
            {
                baloons[i].gameObject.SetActive(true);

                //말풍선 텍스트 갱신
                baloons[i].gameObject.GetComponentInChildren<Text>().text = activatedList[thisIndex];

                baloons[i].onClick.RemoveAllListeners();
                baloons[i].onClick.AddListener(() =>
                {
                    DataManager.GetData().dialogs[GetIDWithTitle(activatedList[thisIndex])] = 2;
                    DataManager.Save();
                    ChatSystem2.instance.StartChat("Dialog", activatedList[thisIndex], () => { });
                    SubBaloon(thisIndex);
                    SetBaloons(index);
                    //actions[thisIndex]();
                    //해당 상호작용 클릭시 실행할 동작 지정
                });
            }
            else
            {
                baloons[i].gameObject.SetActive(false);
            }
        }
    }

    //다이얼로그 xml에서 현재 활성화된 다이얼로그 가져오기
    string GetTitleWithID(int id)
    {
        TextAsset textAsset = (TextAsset)Resources.Load("ChatDB/Dialog");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNode chatList = xmlDoc.SelectSingleNode("Chat");
        return chatList.ChildNodes[id].Name;
    }

    int GetIDWithTitle(string title)
    {
        return titleToID[title];
    }
    public static void InitDialogList()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("ChatDB/Dialog");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNode chatList = xmlDoc.SelectSingleNode("Chat");

        for(int i = 0; i < chatList.ChildNodes.Count; i++)
        {
            if(!titleToID.ContainsKey(chatList.ChildNodes[i].Name))
                titleToID.Add(chatList.ChildNodes[i].Name, i);
        }        
    }
}
