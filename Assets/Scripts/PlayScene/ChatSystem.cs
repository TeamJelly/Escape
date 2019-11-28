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


    public chatter[] chatList;


    public bool startMessage;
    private bool isSkipped = false;
    public bool MoveNextScene = false;
    public string nextSceneName;
    //private GameObject defaultUI;

    void OnEnable()
    {
        StartCoroutine(StartChat());
    }

    IEnumerator StartChat()
    {

        int checkNum = 0;
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0;
        chatUI.SetActive(true);
        while (checkNum < chatList.Length && !isSkipped)
        {
            chatTxt.text = chatList[checkNum].talkString;
            Chatter[chatList[checkNum].who].SetActive(true);
            while (!isSkipped)
            {
                if (Input.touchCount != 0 && Input.GetTouch(0).phase == TouchPhase.Began) break;
                yield return null;
            }
            yield return new WaitForSecondsRealtime(0.1f);
            Chatter[chatList[checkNum].who].SetActive(false);
            checkNum++;
        }

        chatUI.SetActive(false);
        Time.timeScale = 1;
        //defaultUI.SetActive(true);
        if (MoveNextScene) MoveNext();
    }
    public void MoveNext()
    {
        GetComponent<UIFunctions>().NextEvent(nextSceneName);
        //데이터 로딩
    }
    public void SkipChat()
    {
        isSkipped = true;
    }
}
