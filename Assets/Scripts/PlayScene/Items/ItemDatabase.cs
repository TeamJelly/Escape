using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static Item[] itemList = new Item[100];

    public static void InitItemList()
    {
        itemList[1] =
            new Item
            {
                itemName = "이상한 화분",
                itemDescription = "몬생긴 화분이다.",
                ID = 1
            };
        itemList[2] =
            new Item
            {
                itemName = "신용카드",
                itemDescription = "연이의 신용카드이다.",
                ID = 2
            };
        itemList[3] =
            new Item
            {
                itemName = "부서진 열쇠",
                itemDescription = "연이가 처참히 부숴버린 불쌍한 열쇠다.",
                ID = 3
            };
    }

    public static Item GetItemWithID(int id)
    {
        return itemList[id];
    }
}

[System.Serializable]
public class Item
{
    public string itemName;
    public string itemDescription;
    public int ID;
}


