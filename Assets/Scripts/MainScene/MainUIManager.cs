using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIManager : MonoBehaviour
{


    public GameObject InformUI;

    public void Load()
    {
        DataManager.Load();
        UIFunctions.SelectScene(DataManager.currentData.currentScene);
    }

    public void StartNew()
    {
        DataManager.StartAsNew();
        UIFunctions.SelectScene("Intro");
    }
}
