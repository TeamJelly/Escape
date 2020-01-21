using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
public class ItemDatabase : MonoBehaviour
{
    public static Item[] itemList = new Item[100];
    public static void InitItemList()
    {
        Interpret("ItemDB");
    }
    static void Interpret(string _strSource)
    {
        TextAsset textAsset = (TextAsset)Resources.Load(_strSource);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList xmlNodeList = xmlDoc.SelectNodes("Inventory");
        foreach (XmlNode node in xmlNodeList)
        {
            if (node.Name.Equals("Inventory") && node.HasChildNodes)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    int id = Convert.ToInt32(child.Attributes.GetNamedItem("id").Value);
                    itemList[id] = new Item
                    {
                        itemName = child.Attributes.GetNamedItem("name").Value,
                        itemDescription = child.Attributes.GetNamedItem("description").Value,
                        ID = id
                    };
                }

            }

        }
    }
    public static Item GetItemWithID(int id)
    {
        return itemList[id];
    }
}

[System.Serializable]
public class Item
{
    public string itemName; //아이템 이름
    public string itemDescription; //아이템 설명
    public int ID; // 아이템 고유ID
}


