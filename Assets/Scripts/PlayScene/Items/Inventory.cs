using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public GameObject InventoryItemList;
    public GameObject InventoryBar;
    public GameObject ItemBoxPrefab;
    
    public static Inventory instance;

    public string selectedItem;
    public System.Action interactMethod;
    Dictionary<string, GameObject> objFinder = new Dictionary<string, GameObject>();
    public void Awake()
    {
        instance = this;
    }
    public void GetItem(string itemName)
    {
        PlayUIManager.instance.NoticeGetItem(itemName);
        AddItem(itemName);
        DataManager.GetData().items[ItemDatabase.GetItemWithName(itemName).ID] = 1;
        DataManager.Save_Auto();
    }
    public void AddItem(int itemID)
    {
        Item item = ItemDatabase.GetItemWithID(itemID);
        AddItem(item);
    }

    public void AddItem(string itemName)
    {
        Item item = ItemDatabase.GetItemWithName(itemName);
        AddItem(item);
    }
    void AddItem(Item item)
    {
        if (objFinder.ContainsKey(item.itemName)) return;
        
        GameObject newItemBox = Instantiate(ItemBoxPrefab);

        newItemBox.transform.SetParent(InventoryItemList.transform);

        RectTransform rect = (RectTransform)newItemBox.transform;
        rect.localScale = Vector2.one;
        newItemBox.GetComponent<Slot>().Init(item);

        
        objFinder.Add(item.itemName, newItemBox);
    }
    public void SubItem(int itemID)
    {
        Item item = ItemDatabase.GetItemWithID(itemID);
        SubItem(item);
    }

    public void SubItem(string itemName)
    {
        Item item = ItemDatabase.GetItemWithName(itemName);
        SubItem(item);
    }

    public void SubItem(Item item)
    {
        DataManager.GetData().items[item.ID] = 2;
        DataManager.Save_Auto();
        Destroy(objFinder[item.itemName]);
        objFinder.Remove(item.itemName);
    }

    public void EnableInventoryBar()
    {
        InventoryBar.SetActive(true);
    }
    public void DisableInventoryBar()
    {
        InventoryBar.SetActive(false);
    }
    
}
