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

    private bool isSkipped = false;

    public void StartChat(System.Action onEnd)
    {
        this.onEnd = onEnd;
        StartCoroutine(loop());
    }
    IEnumerator loop()
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
        onEnd();
    }
    public void SkipChat()
    {
        isSkipped = true;
    }
}
