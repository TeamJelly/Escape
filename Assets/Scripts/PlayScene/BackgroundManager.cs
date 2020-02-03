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
                ChatSystem2.instance.StartChat(2,() => GetQuest(0));
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

        foreach (Item item in ItemDatabase.itemList)
        {
            if (item == null) continue;
            //Debug.Log(item.itemName);
            if (data.items[item.ID] == 1)
            {
                Inventory.instance.AddItem(item.ID);
            }
        }
    }

    //아이템오브젝트에서 콜백 형식으로 호출됨.
    public void GetItem(int itemID) 
    {
        data.items[itemID] = 1;
        DataManager.Save();
        Inventory.instance.AddItem(itemID);
        PlayUIManager.instance.NoticeGetItem(itemID);
    }

    public void GetQuest(int eventID)
    {
        data.events[eventID] = 1;
        DataManager.Save();
    }
    public void FinishQuest(int eventID)
    {
        data.events[eventID] = 2;
        DataManager.Save();
    }
}
