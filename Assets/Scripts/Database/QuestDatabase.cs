using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Xml;


enum QuestType {낮,밤};
public static class QuestDatabase
{

    public static Quest[] QList = new Quest[100];

    static Dictionary<string, Quest> dictionary = new Dictionary<string, Quest>();
    public static void InitQuestLists()
    {
        Interpret("QuestDB");

        foreach(Quest quest in QList)
        {
            if(quest != null&& !dictionary.ContainsKey(quest.title))
            dictionary.Add(quest.title, quest);
        }
    }
    static void Interpret(string _strSource)
    {
        TextAsset textAsset = (TextAsset)Resources.Load(_strSource);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNode quests = xmlDoc.SelectSingleNode("Quest");
        foreach (XmlNode node in quests.ChildNodes)
        {
            int id = Convert.ToInt32(node.Attributes.GetNamedItem("id").Value);
            QList[id] = new Quest
            {
                title = node.Attributes.GetNamedItem("title").Value,
                problem = node.Attributes.GetNamedItem("problem").Value,
                description = node.Attributes.GetNamedItem("description").Value,
                ID = id
            };

        }
    }
    public static Quest GetQuestWithID(int ID)
    {
        return QList[ID];
    }
    public static Quest GetQuestWithTitle(string qName)
    {
        return dictionary[qName];
    }
}
public class Quest
{
    QuestType type;
    public string title; // 이벤트 제목
    public string problem; // 이벤트 문제
    public int ID; // 이벤트 고유 ID
    public string description; // 이벤트 설명

}
