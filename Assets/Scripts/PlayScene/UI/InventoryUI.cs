using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Button upButton;
    public Button downButton;

    public RectTransform itemListUI;
    public GameObject itemBoxPrefab;
    public static InventoryUI instance;
    public GameObject thisUI;

    int topindex = 0;
    int downIndex = 0;
    int maxSize = 0;
    float remained = 0;
    public int itemNum = 0;
    private void Awake()
    {
        instance = this;
        upButton.gameObject.SetActive(false);
        downButton.gameObject.SetActive(false);
        upButton.onClick.AddListener(Up);
        downButton.onClick.AddListener(Down);
        topindex = 0;
        

    }
    private void Start()
    {
        maxSize = (int)((((RectTransform)thisUI.transform).rect.height) / 160);
        remained = ((((RectTransform)thisUI.transform).rect.height) % 160);
    }
    public GameObject AddBox(Item item)
    {
        GameObject newItemBox = Instantiate(itemBoxPrefab);

        newItemBox.transform.SetParent(itemListUI);

        RectTransform rect = (RectTransform)newItemBox.transform;
        rect.localScale = Vector2.one;
        newItemBox.GetComponent<Slot>().Init(item);

        if(downIndex - topindex <= maxSize)
        {
            downIndex++;
        }
        else
        {
            downButton.gameObject.SetActive(true);
        }
        itemNum++;
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
    void Up()
    {
        topindex--;
        downIndex--;
        //if(downIndex + 1 == itemNum)
        //{
        //    itemListUI.anchoredPosition -= Vector2.up * (30);
        //}
        if(topindex == 0)
        {
            itemListUI.anchoredPosition = Vector2.zero;
        }
        else itemListUI.anchoredPosition -= Vector2.up * 160;
        if (topindex == 0) upButton.gameObject.SetActive(false);
        downButton.gameObject.SetActive(true);
    }

    void Down()
    {
        topindex++;
        downIndex++;
        //if (topindex == 1)
        //{
        //    itemListUI.anchoredPosition = Vector2.up * ((160 - remained) + 100);
        //}
        if (downIndex == itemNum)
        {
            itemListUI.anchoredPosition = Vector2.up * (itemListUI.rect.height - ((RectTransform)thisUI.transform).rect.height);
        }
        else itemListUI.anchoredPosition += Vector2.up * 160;
        if (downIndex >= itemNum) downButton.gameObject.SetActive(false);
        upButton.gameObject.SetActive(true);
        Debug.Log(maxSize);
    }

}
