using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_M2 : MonoBehaviour
{
    private void OnEnable()
    {
        //이전 메인이벤트가 현재 진행중이면 완료로 변경,
        //그 다음 메인 이벤트를 현재 진행중으로 변경,
        //화면상에 띄우기.
        if(DataManager.currentData.mainEvents[10] == 1)
        {
            DataManager.currentData.mainEvents[10] = 2;
            DataManager.currentData.mainEvents[20] = 1;
            DataManager.Save();
            QuestUIManager.instance.Enable(QuestDatabase.MainQList[20]);
        }
    }
}
