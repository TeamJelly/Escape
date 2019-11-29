using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class BackgroundManager : MonoBehaviour
{
    public List<Button> allNodes = new List<Button>();
    public List<ItemObject> items = new List<ItemObject>();
    public List<Puzzle> puzzles = new List<Puzzle>();
    public static BackgroundManager instance;

    public GameObject notifyPanel;
    PlayerData data;

    private void Awake()
    {
        data = DataManager.currentData;
        QuestDatabase.InitQuestLists();
        ItemDatabase.InitItemList();

        GameObject[] _items = GameObject.FindGameObjectsWithTag("Item");

        foreach(GameObject i in _items)
        {
            items.Add(i.GetComponent<ItemObject>());
        }
    }
    private void Start()
    {
        foreach(ItemObject itemObj in items)
        {
            itemObj.item = ItemDatabase.GetItemWithID(itemObj.itemID);
            if (data.items[itemObj.item.ID] > 0)
                itemObj.DisableItem();
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
