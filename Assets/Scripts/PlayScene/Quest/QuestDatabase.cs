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
        //MainQList[10] = 
        //    new Quest
        //    {
        //        title = "[현관 탐색]",
        //        problem = "집에서 탈출하기 위해서는 현관을 가야하는데… 몰래 현관으로 가는 방법이 없을까?",
        //        state = 0,
        //        ID = 10,
        //        description = "현관에 잠입하는데 성공한 주인공의 마음은 탈출할수 있다는 희망에 부풀어 있었다. " +
        //        "그러나 주인공의 기대와는 달리 현관문은 4개의 잠금장치로 단단히 잠겨있었고," +
        //        " 밀려오는 절망감에 빠져있는 주인공의 옆에서 연이는 ‘탈출할수 없을꺼야’라고 말한다. "

        //    };
        //MainQList[20] =
        //   new Quest
        //   {
        //       title = "[현관 문의 4가지 잠금장치]",
        //       problem = "현관에 들어가는데는 성공했지만 역시나 문에는 여러개의 잠금장치가 있었다.",
        //       state = 0,
        //       ID = 20,
        //       description = "의지를 다진 주인공은 연이의 주의를 돌린 틈을타서 다시 현관문 자물쇠를 천천히 관찰했다."

        //   };

        //MainQList[21] =
        //  new Quest
        //  {
        //      title = "[첫번째 자물쇠]",
        //      problem = "하트모양 열쇠식 자물쇠다. 열쇠 하나는 연이가 항상 지니고 있고, 여분열쇠는 연이가 부러뜨려 쓰레기통에 버렸다.",
        //      state = 0,
        //      ID = 21,
        //      description = ""

        //  };

        //MainQList[22] =
        //  new Quest
        //  {
        //      title = "[두번째 자물쇠]",
        //      problem = "4자리 숫자의 조합으로 되어있는 자물쇠이다.",
        //      state = 0,
        //      ID = 22,
        //      description = ""

        //  };
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
    public string title;
    public string problem;
    public int state; //0이면안받은 상태, 1이면 수행중, 2이면 완료
    public int ID;
    public string description;

}

class MainQuest : Quest
{
    int ChapterNum;
}

class SubQuest : Quest
{

}
