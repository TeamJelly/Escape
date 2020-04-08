using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;

    public RectTransform itemListUI;
    public GameObject itemBoxPrefab;
    public GameObject thisUI;
    private void Awake()
    {
        instance = this;
    }
    public GameObject AddBox(Item item)
    {
        GameObject newItemBox = Instantiate(itemBoxPrefab);

        newItemBox.transform.SetParent(itemListUI);

        RectTransform rect = (RectTransform)newItemBox.transform;
        rect.localScale = Vector2.one;
        newItemBox.GetComponent<Slot>().Init(item);
        return newItemBox;
    }
    public void EnableInventoryBar()
    {
        thisUI.SetActive(true);
    }
    public void DisableInventoryBar()
    {
        thisUI.SetActive(false);
    }
}