using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestJoint : MonoBehaviour
{
    public int prevEventID;
    public int nextEventID;
    public QuestType type;
    private void OnEnable()
    {
        //이전 메인이벤트가 현재 진행중이면 완료로 변경,
        //그 다음 메인 이벤트를 현재 진행중으로 변경,
        //화면상에 띄우기.
        if (DataManager.currentData.events[(int)type,prevEventID] == 1)
        {
            BackgroundManager.instance.FinishQuest(type, prevEventID);
            BackgroundManager.instance.GetQuest(type, nextEventID
                );
        }
    }
}
