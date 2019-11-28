using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzle : MonoBehaviour
{
    public bool isCleared = false;
    public bool isShown = false;
    public bool isMain = true;
    public int eventID;
    public QuestUIManager ui;

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
            ui.Enable(QuestDatabase.MainQList[eventID]);
        }
        else if (eventState == 2) OnEnd();
    }
}
