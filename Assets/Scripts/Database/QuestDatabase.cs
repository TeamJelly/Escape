using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Xml;


public enum QuestType {Main, Sub};
public static class QuestDatabase
{
    static Quest[,] QList = new Quest[2,100];

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
                    QList[0, id] = new Quest
                    {
                        type = QuestType.Main,
                        title = child.Attributes.GetNamedItem("title").Value,
                        problem = child.Attributes.GetNamedItem("problem").Value,
                        description = child.Attributes.GetNamedItem("description").Value,
                        ID = id
                    };
                }

            }

        }
    }
    public static Quest GetQuestWithID(QuestType type, int ID)
    {
        return QList[(int)type, ID];
    }
}
public class Quest
{
    public QuestType type;
    public string title; // 이벤트 제목
    public string problem; // 이벤트 문제
    public int ID; // 이벤트 고유 ID
    public string description; // 이벤트 설명

}
