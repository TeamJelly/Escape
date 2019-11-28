using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class BackgroundManager : MonoBehaviour
{
    public List<Button> allNodes = new List<Button>();
    public List<Item> items = new List<Item>();
    public List<Puzzle> puzzles = new List<Puzzle>();
    public static BackgroundManager instance;

    public GameObject notifyPanel;
    public PlayerData data;

    private void Awake()
    {
        data = DataManager.currentData;
        QuestDatabase.InitQuestLists();
    }
    private void Start()
    {
        foreach(Item item in items)
        {
            if (data.items[item.ID] > 0)
                item.DisableItem();
        }
        foreach(Button b in allNodes)
        {
            b.onClick.AddListener(DisableCamMove);
        }

        //여기다가 이벤트 진행사항 불러오기 만들어야 함.
    }

    public void EnableCamMove()
    {
        CamCtrl.instance.isMovable = true;
    }
    public void DisableCamMove()
    {
        CamCtrl.instance.isMovable = false;
    }
}
