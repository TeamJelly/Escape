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
                if (data.events[i] == 2)
                {
                    AddQuest(i);
                    FinishQuest(i);
                }
                else AddQuest(i);
                
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
        if (onEndQuest.ContainsKey(quest.title)) return;

        DataManager.GetData().events[quest.ID] = 1;
        DataManager.Save();
        GameObject newObj = Instantiate(questBoxPrefab);
        newObj.GetComponent<Button>().onClick.AddListener(() =>
        {
            //퀘스트목록에서 선택했을 때 기능.
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

    public void EnableDialog(int id)
    {
        DataManager.GetData().dialogs[id] = 1;
    }
    public void DisableDialog(int id)
    {
        DataManager.GetData().dialogs[id] = 2;
    }
    // Start is called before the first frame update

}
