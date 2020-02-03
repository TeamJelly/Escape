using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStarter : MonoBehaviour
{
    public int eventID;
    private void OnEnable()
    {
        //아직 퀘스트 수주 상태가 아니라면 새 퀘스트 등록.
        if (DataManager.GetData().events[eventID] == 0)
        {
            BackgroundManager.instance.GetQuest(eventID);
        }
    }
}
