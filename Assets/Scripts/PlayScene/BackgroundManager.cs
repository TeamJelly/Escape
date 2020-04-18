using System.Collections;
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
    public int failureCount = 0;
    public bool isPaused = false;

    private void Awake()
    {
        instance = this;
        data = DataManager.GetData();
    }

    private void Start()
    {
        
//        InitItems();
//        DataManager.Save_Auto();
        //if (data.events[0] == 0)
        //{
        //    DataManager.Save();          
        //    PlayUIManager.instance.FadeIn(() =>
        //    {
        //        ChatSystem2.instance.StartChat("Intro","I2",() =>
        //        {
        //            QuestManager.instance.AddQuest("[현관문 진입]");
        //            //Debug.Log("Quest Added");
        //            });
        //    });
        //}            
    }
    private void InitItems()
    {
        //맵상에 있는 모든 아이템 찾기
        GameObject[] _items = GameObject.FindGameObjectsWithTag("Item");

        //해당 아이템이 수집상태 or 사용된 상태라면 비활성화.
        foreach (GameObject i in _items)
        {
            ItemObject itemObj = i.GetComponent<ItemObject>();
            itemObj.Init();
            if (data.items[ItemDatabase.GetItemWithName(itemObj.itemName).ID] > 0)
            {
                itemObj.DisableItem();
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

    public void AddFailureCount()
    {
        failureCount++;
        if (failureCount >= 5)
        {
            //게임오버
        }
    }
}
