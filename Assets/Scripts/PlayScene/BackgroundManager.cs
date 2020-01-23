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
       

        GameObject[] _items = GameObject.FindGameObjectsWithTag("Item");
        foreach(GameObject i in _items)
        {
            items.Add(i.GetComponent<ItemObject>());
        }
    }
    private void Start()
    {
        if (data.events[0] == 0)
        {
            DataManager.Save();          
            PlayUIManager.instance.FadeIn(() =>
            {
                ChatSystem2.instance.StartChat(2,
                  () =>
                  {
                      GetQuest(0);
                  });
            });
        }
        foreach (ItemObject itemObj in items)
        {
            itemObj.item = ItemDatabase.GetItemWithID(itemObj.itemID);
            if (data.items[itemObj.item.ID] > 0)
                itemObj.DisableItem();
        }
    }
    public void GetItem(Item item) { data.items[item.ID] = 1; }

    public void GetQuest(int eventID)
    {
        data.events[eventID] = 1;
        DataManager.Save();
        QuestUIManager.instance.Enable(QuestDatabase.GetQuestWithID(eventID));
    }
    public void FinishQuest(int eventID)
    {
        data.events[eventID] = 2;
        DataManager.Save();
    }
}
