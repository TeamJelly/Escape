using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public GameObject questListObj;
    public GameObject questBoxPrefab;
    public static QuestManager instance;
    Dictionary<string, System.Action> onEndQuest = new Dictionary<string, System.Action>();
    private void Awake()
    {
        instance = this;
        InitQuestList();
    }


    public void InitQuestList()
    {
        PlayerData data = DataManager.GetData();
       for (int i = 0; i < data.events.Length; i++)
        {
            if(data.events[i] > 0)
            {
                AddQuest(i);
                if (data.events[i] == 2)
                    FinishQuest(i);
            }
        }
    }
    public void AddQuest(int id)
    {
        AddQuest(QuestDatabase.GetQuestWithID(id));
    }

    public void AddQuest(string qTitle)
    {
        AddQuest(QuestDatabase.GetQuestWithTitle(qTitle));
    }

    public void AddQuest(Quest quest)
    {
        DataManager.GetData().events[quest.ID] = 1;
        DataManager.Save();
        GameObject newObj = Instantiate(questBoxPrefab);
        newObj.GetComponent<Button>().onClick.AddListener(() =>
        {

        });

        newObj.GetComponentInChildren<Text>().text = quest.title;
        onEndQuest.Add(quest.title, () =>
         {
             newObj.GetComponent<Button>().onClick.RemoveAllListeners();
             newObj.GetComponentInChildren<Text>().text += "-완료";
         });
        newObj.transform.SetParent(questListObj.transform);
    }

    public void FinishQuest(int id)
    {
        FinishQuest(QuestDatabase.GetQuestWithID(id));
    }
    public void FinishQuest(string name)
    {
        FinishQuest(QuestDatabase.GetQuestWithTitle(name));
    }
    public void FinishQuest(Quest quest)
    {
        DataManager.GetData().events[quest.ID] = 2;
        DataManager.Save();
        if(!onEndQuest.ContainsKey(quest.title))
        {
            AddQuest(quest);
        }
        onEndQuest[quest.title]();
    }
    // Start is called before the first frame update

}
