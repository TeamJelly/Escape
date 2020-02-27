using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject InventoryItemList;
    public GameObject ItemBoxPrefab;
    public static Inventory instance;

    public Slot selectedSlot = null;
    public System.Action interactMethod;
    Dictionary<string, GameObject> objFinder = new Dictionary<string, GameObject>();
    public void Awake()
    {
        instance = this;
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
        DataManager.GetData().items[item.ID] = 1;
        DataManager.Save();
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
        DataManager.Save();
        Destroy(objFinder[item.itemName]);
        objFinder.Remove(item.itemName);
    }

    
}
