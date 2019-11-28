using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public abstract class Item : MonoBehaviour
{
    public string itemName;
    public string itemDescription;
    public int ID;
    public List<GameObject> images = new List<GameObject>();

    public void DisableItem()
    {
        foreach (GameObject img in images)
        {
            img.SetActive(false);
        }
        gameObject.SetActive(false);
    }
    public void Get()
    {
        DataManager.currentData.items[ID] = 1;
        DataManager.Save();
        PlayUIManager.instance.GetItem(this);
        DisableItem();
    }
}
