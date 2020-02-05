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
        DataManager.GetData().items[item.ID] = 1;
        DataManager.Save();
        GameObject newItemBox = Instantiate(ItemBoxPrefab);

        newItemBox.transform.SetParent(InventoryItemList.transform);

        newItemBox.GetComponent<Slot>().Init(item);
    }

    
}
