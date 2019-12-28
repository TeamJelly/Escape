using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Xml;
public static class QuestDatabase
{
    public static Quest[] MainQList = new Quest[100];
    public static Quest[] SubQList = new Quest[100];

    public static void InitQuestLists()
    {
        Interpret("QuestDB");
    }
    static void Interpret(string _strSource)
    {
        TextAsset textAsset = (TextAsset)Resources.Load(_strSource);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList xmlNodeList = xmlDoc.SelectNodes("Quest");
        foreach (XmlNode node in xmlNodeList)
        {
            if (node.Name.Equals("Quest") && node.HasChildNodes)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    int id = Convert.ToInt32(child.Attributes.GetNamedItem("id").Value);
                    MainQList[id] = new Quest
                    {
                        title = child.Attributes.GetNamedItem("title").Value,
                        problem = child.Attributes.GetNamedItem("problem").Value,
                        description = child.Attributes.GetNamedItem("description").Value,
                        state = 0,
                        ID = id
                    };
                }

            }

        }
    }
    public static Quest GetQuestWithID(string type, int ID)
    {
        if (type == "Main")
            return MainQList[ID];
        else return SubQList[ID];
    }
}
public class Quest
{
    public string title; // 이벤트 제목
    public string problem; // 이벤트 문제
    public int state; //0이면안받은 상태, 1이면 수행중, 2이면 완료
    public int ID; // 이벤트 고유 ID
    public string description; // 이벤트 설명

}

class MainQuest : Quest
{
    int ChapterNum;
}

class SubQuest : Quest
{

}
