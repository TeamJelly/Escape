using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class BackgroundManager : MonoBehaviour
{
    public List<ItemObject> items = new List<ItemObject>();
    public static BackgroundManager instance;

    PlayerData data;

    private void Awake()
    {
        instance = this;
        data = DataManager.currentData;
        if (data.events[(int)QuestType.Main,10] == 0)
        {
            DataManager.currentData.currentScene = "ForNewUISystem";
            GetComponent<ChatSystem>().StartChat(
                   () =>
                   {
                       GetQuest(QuestType.Main, 10);
                   });
        }

        GameObject[] _items = GameObject.FindGameObjectsWithTag("Item");
        foreach(GameObject i in _items)
        {
            items.Add(i.GetComponent<ItemObject>());
        }
    }
    private void Start()
    {
        foreach (ItemObject itemObj in items)
        {
            itemObj.item = ItemDatabase.GetItemWithID(itemObj.itemID);
            if (data.items[itemObj.item.ID] > 0)
                itemObj.DisableItem();
        }
    }
    public void AddHeart(int i) { data.Heart += i; }
    public void SubHeart(int i) { data.Heart -= i; }
    public void AddTime(int i) { data.Time += i; }
    public void SubTime(int i) { data.Time -= i; }

    public void GetItem(Item item) { data.items[item.ID] = 1; }

    public void GetQuest(QuestType type, int eventID)
    {
        data.events[(int)type, eventID] = 1;
        DataManager.Save();
        QuestUIManager.instance.Enable(QuestDatabase.GetQuestWithID(type,eventID));
    }
    public void FinishQuest(QuestType type, int eventID)
    {
        data.events[(int)type,eventID] = 2;
        DataManager.Save();
    }
}
