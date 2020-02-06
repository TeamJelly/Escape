﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

[Serializable]
public class BackgroundManager : MonoBehaviour
{
    //public List<ItemObject> items = new List<ItemObject>();
    public static BackgroundManager instance;

    PlayerData data;

    private void Awake()
    {
        instance = this;
        data = DataManager.GetData();
        
    }
    private void Start()
    {
        InitItems();
        if (data.events[0] == 0)
        {
            DataManager.Save();          
            PlayUIManager.instance.FadeIn(() =>
            {
                ChatSystem2.instance.StartChat("Intro","I2",() =>
                {
                    QuestManager.instance.AddQuest(0);
                    Debug.Log("Quest Added");
                    });
            });
        }            
    }
    private void InitItems()
    {
        //맵상에 있는 모든 아이템 찾기
        GameObject[] _items = GameObject.FindGameObjectsWithTag("Item");

        //해당 아이템이 수집상태 or 사용된 상태라면 비활성화.
        foreach (GameObject i in _items)
        {
            ItemObject itemObj = i.GetComponent<ItemObject>();
            if (data.items[itemObj.itemID] > 0)
            {
                itemObj.DisableItem();
            }
            else
            {
                itemObj.Init();
            }
        }
        for(int i = 0; i < data.items.Length; i++)
        {
            if(data.items[i] == 1)
            {
                Inventory.instance.AddItem(i);
            }
        }
    }

    
}
