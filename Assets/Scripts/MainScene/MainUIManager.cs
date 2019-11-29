using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIManager : MonoBehaviour
{


    public GameObject InformUI;
    UIFunctions uiFuncs;

    private void Awake()
    {
        uiFuncs = GetComponent<UIFunctions>();
    }
    public void Load()
    {
        DataManager.Load();
        uiFuncs.SelectScene(DataManager.currentData.currentScene);
    }

    public void StartNew()
    {
        DataManager.StartAsNew();
        uiFuncs.SelectScene("Intro");
    }
}
