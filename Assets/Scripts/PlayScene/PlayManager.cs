using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public static PlayManager instance;

    public PlayerData data;

    public Item usingItem;


    
    private void Awake()
    {
        instance = this;
        data = DataManager.currentData;

        
    }
    private void Start()
    {
        if(data.mainEvents[10] == 0)
        {
            DataManager.currentData.currentScene = "ForNewUISystem";
            GetComponent<ChatSystem>().StartChat(
                   () =>
                   {
                       GetQuest("Main", 10);
                   });
        }
    }
    public void AddHeart(int i) { data.Heart += i; }
    public void SubHeart(int i) { data.Heart -= i; }
    public void AddTime(int i) { data.Time += i; }
    public void SubTime(int i) { data.Time -= i; }

    public void GetItem(Item item) { data.items[item.ID] = 1; }

    public void GetQuest(string eventType, int eventID)
    {
        if (eventType == "Main") 
        {
            data.mainEvents[eventID] = 1;
            DataManager.Save();
            QuestUIManager.instance.Enable(QuestDatabase.MainQList[eventID]);
        }
        else if (eventType == "Sub") data.subEvents[eventID] = 1;
        
    }
    public void FinishQuest(string eventType, int eventID)
    {
        if (eventType == "Main") data.mainEvents[eventID] = 2;
        else if (eventType == "Sub") data.subEvents[eventID] = 2;
    }
}
