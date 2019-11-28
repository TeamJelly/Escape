using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_M2 : MonoBehaviour
{
    public QuestUIManager ui;
    private void OnEnable()
    {
        if(DataManager.currentData.mainEvents[10] == 1)
        {
            DataManager.currentData.mainEvents[10] = 2;
            DataManager.currentData.mainEvents[20] = 1;
            DataManager.Save();
            ui.Enable(QuestDatabase.MainQList[20]);
        }
    }
}
