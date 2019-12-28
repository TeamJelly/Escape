using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzle : MonoBehaviour
{
   // public bool isCleared = false; // 
   // public bool isShown = false; // 
    public bool isMain = true; // 메인이벤트면 true 서브이벤트면 false
    public int eventID;


    //퍼즐 활성화 하기 전 이전에 퍼즐을 본 경험이 있는지 깼었는지 채크. 
    public void OnEnable()
    {
        CheckEventState();
    }
    public abstract void OnEnd();
    public void CheckEventState()
    {
        int eventState = DataManager.currentData.mainEvents[eventID];
        if (eventState == 0)
        {
            DataManager.currentData.mainEvents[eventID] = 1;
            DataManager.Save();
            QuestUIManager.instance.Enable(QuestDatabase.MainQList[eventID]);
        }
        else if (eventState == 2) OnEnd();
    }
}
