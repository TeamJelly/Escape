using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : UIFunctions
{
    public GameObject currentUI;

    public GameObject InformUI;


    public new void Load()
    {
        base.Load();
        if (DataManager.currentData == null)
        {
            InformUI.SetActive(true);
        }
        else SelectScene(DataManager.currentData.currentScene);

    }
    public void SwabCurrent(GameObject g)
    {
        currentUI.SetActive(false);
        currentUI = g;
        currentUI.SetActive(true);
    }
    public void StartNew()
    {
        DataManager.StartAsNew();
        SelectScene("Intro");
    }
}
