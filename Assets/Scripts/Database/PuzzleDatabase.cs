using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Xml;
public static class PuzzleDatabase
{

    static Dictionary<string, int> dictionary = new Dictionary<string, int>();
    public static void InitPuzzleList()
    {
        Interpret("PuzzleDB");
    }
    static void Interpret(string _strSource)
    {
        TextAsset textAsset = (TextAsset)Resources.Load(_strSource);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNode quests = xmlDoc.SelectSingleNode("Puzzles");
        foreach (XmlNode node in quests.ChildNodes)
        {
            int id = Convert.ToInt32(node.Attributes.GetNamedItem("id").Value);
            dictionary.Add(node.Attributes.GetNamedItem("name").Value, id);
        }
    }
    public static int GetPuzzleID(string name)
    {
        return dictionary[name];
    }
    public static void SetPuzzleState(string name, int state)
    {
        int id = GetPuzzleID(name);
        if (DataManager.GetData().puzzles[id] < state)
        {
            DataManager.GetData().puzzles[id] = state;
            DataManager.Save_Auto();
        }
    }
    public static int GetPuzzleState(string name)
    {
        return DataManager.GetData().puzzles[GetPuzzleID(name)];
    }
}