using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
public class ItemDatabase : MonoBehaviour
{
    public static Item[] itemList = new Item[100];
    static Dictionary<string, Item> dictionary = new Dictionary<string, Item>();
    public static void InitItemList()
    {
        Interpret("ItemDB");
        foreach (Item item in itemList)
        {
            if(item != null&& !dictionary.ContainsKey(item.itemName))
            dictionary.Add(item.itemName, item);
        }
    }
    static void Interpret(string _strSource)
    {
        TextAsset textAsset = (TextAsset)Resources.Load(_strSource);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNode items = xmlDoc.SelectSingleNode("Items");
        foreach (XmlNode node in items.ChildNodes)
        {
            int id = Convert.ToInt32(node.Attributes.GetNamedItem("id").Value);
            itemList[id] = new Item
            {
                itemName = node.Attributes.GetNamedItem("name").Value,
                itemDescription = node.Attributes.GetNamedItem("description").Value,
                ID = id
            };
        }
    }
    public static Item GetItemWithID(int id)
    {
        return itemList[id];
    }
    public static Item GetItemWithName(string iName)
    {
        return dictionary[iName];
    }
}

[System.Serializable]
public class Item
{
    public string itemName; //아이템 이름
    public string itemDescription; //아이템 설명
    public int ID; // 아이템 고유ID
}


