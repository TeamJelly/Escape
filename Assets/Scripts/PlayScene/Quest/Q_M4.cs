using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_M4 : MonoBehaviour
{
    public QuestUIManager ui;
    public int eventID;
    private void OnEnable()
    {

        if (DataManager.currentData.mainEvents[eventID] == 0)
        {
            DataManager.currentData.mainEvents[eventID] = 1;
            DataManager.Save();
            ui.Enable(QuestDatabase.MainQList[eventID]);
        }
    }
}
