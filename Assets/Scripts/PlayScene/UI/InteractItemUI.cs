using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractItemUI : MonoBehaviour
{
    public GameObject thisUI;
    public GameObject panel;
    Button[] slots;
    int[] itemsList;

    public void Awake()
    {
        slots = panel.GetComponentsInChildren<Button>();
        itemsList = DataManager.currentData.items;

    }
    public void Enable()
    {
        
        int slotIdx = 0;
        for (int i = 0; i < itemsList.Length; i++)
        {
            if (itemsList[i] == 1)
            {
                slots[slotIdx].gameObject.GetComponentInChildren<Text>().text = ItemDatabase.GetItemWithID(i).itemName;
                slotIdx++;
            }
        }
       // thisUI.SetActive(true);
    }
}
