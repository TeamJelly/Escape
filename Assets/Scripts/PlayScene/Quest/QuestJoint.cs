using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestJoint : MonoBehaviour
{
    public int prevEventID;
    public int nextEventID;
    public int chatNum = -1;
    private void OnEnable()
    {
        //이전 메인이벤트가 현재 진행중이면 완료로 변경,
        //그 다음 메인 이벤트를 현재 진행중으로 변경,
        //화면상에 띄우기.
        if (DataManager.currentData.events[prevEventID] == 1)
        {
            System.Action act = () =>
            {
              BackgroundManager.instance.FinishQuest(prevEventID);
              BackgroundManager.instance.GetQuest(nextEventID);
            };
            if (chatNum >= 0)
            {
                ChatSystem.instance.StartChat(chatNum, act);
            }
            else act();
        }
    }
}
