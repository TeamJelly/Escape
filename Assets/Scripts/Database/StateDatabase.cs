using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Xml;

//enum QuestType {낮,밤};
public static class StateDatabase
{
    public static State[] SList = new State[100];

    static Dictionary<string, State> dictionary = new Dictionary<string, State>();
    public static void InitStateLists()
    {
        Interpret("StateDB");
        foreach(State state in SList)
        {
            if(state != null&& !dictionary.ContainsKey(state.title))
            dictionary.Add(state.title, state);
        }
    }

    static void Interpret(string _strSource)
    {
        TextAsset textAsset = (TextAsset)Resources.Load(_strSource);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNode quests = xmlDoc.SelectSingleNode("State");
        foreach (XmlNode node in quests.ChildNodes)
        {
            int id = Convert.ToInt32(node.Attributes.GetNamedItem("id").Value);
            SList[id] = new State
            {
                title = node.Attributes.GetNamedItem("title").Value,
                //description = node.Attributes.GetNamedItem("description").Value,
                ID = id
            };
        }
    }

    public static State GetStateWithID(int ID)
    {
        return SList[ID];
    }
    public static State GetStateWithTitle(string qName)
    {
        return dictionary[qName];
    }
    public static int GetStateValue(string name)
    {
        return DataManager.GetData().events[GetStateWithTitle(name).ID];
    }
}
public class State
{
    public string title; // 이벤트 제목
    public int ID; // 이벤트 고유 ID
    //public string description; // 이벤트 설명
}
