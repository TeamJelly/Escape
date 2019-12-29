﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//메인화면 진입시 가장 먼저 이루어지는 초기화작업.
public class Starter : MonoBehaviour
{
    public GameObject LoadButton;
    
    private void Awake()
    {
        Debug.Log(Application.persistentDataPath);
        DataManager.Load();
        QuestDatabase.InitQuestLists();
        ItemDatabase.InitItemList();
        if (DataManager.currentData == null)
            LoadButton.SetActive(false);
       
    }
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
