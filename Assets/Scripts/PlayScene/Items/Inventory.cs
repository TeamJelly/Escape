using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject InventoryItemList;
    public GameObject ItemBoxPrefab;
    public static Inventory instance;

    public int selectedItemID =  -1;
    public System.Action interactMethod;
    public void Awake()
    {
        instance = this;
    }
    public void AddItem(int itemID)
    {
        GameObject newItemBox = Instantiate(ItemBoxPrefab);
        
        newItemBox.transform.SetParent(InventoryItemList.transform);

        newItemBox.GetComponent<Slot>().Init(itemID);
    }

    
}
