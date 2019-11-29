using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public int itemID;
    public List<GameObject> images = new List<GameObject>();
    public Item item;

    public void DisableItem()
    {
        foreach (GameObject img in images)
        {
            img.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
