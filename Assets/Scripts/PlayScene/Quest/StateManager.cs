using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    public GameObject questListObj;
    public GameObject questBoxPrefab;
    public static StateManager instance;

//    Dictionary<string, System.Action> onEndQuest = new Dictionary<string, System.Action>();

    private void Awake()
    {
        instance = this;
        //InitQuestList();
    }

    public void EnableState(string s)
    {
        int stateID = StateDatabase.GetStateWithTitle(s).ID;
        if(DataManager.GetData().events[stateID] < 1)
        DataManager.GetData().events[stateID] = 1;
    }
    public void EndState(string s)
    {
        DataManager.GetData().events[StateDatabase.GetStateWithTitle(s).ID] = 2;
    }

    public void EnableEncount()
    {
        EnableState("[연이 출몰_침실]");
        EnableState("[연이 출몰_다용도실]");
        EnableState("[연이 출몰_화장실]");
        EnableState("[연이 출몰_연이방]");
        EnableState("[연이 출몰_현관]");
    }

        /*
     
   public void InitQuestList()
   {
       PlayerData data = DataManager.GetData();
      for (int i = 0; i < data.events.Length; i++)
       {
           if(data.events[i] > 0)
           {
               if (data.events[i] == 2)
               {
                   FinishQuest(i);
               }
               else AddQuest(i);

           }
       }
   }



   public void GetQuest(string qTitle)
   {
       AddQuest(QuestDatabase.GetQuestWithTitle(qTitle));
       DataManager.Save_Auto();
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
       RectTransform rect = (RectTransform)newObj.transform;
       rect.localScale = Vector2.one;
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

       if(!onEndQuest.ContainsKey(quest.title))
       {
           AddQuest(quest);
           onEndQuest[quest.title]();
       }
       else
       {
           DataManager.GetData().events[quest.ID] = 2;
           DataManager.Save_Auto();
       }

   }

   public void EnableDialog(int id)
   {
       if (DataManager.GetData().dialogs[id] == 0)
        DataManager.GetData().dialogs[id] = 1;
       DataManager.Save_Auto();
   }
   public void DisableDialog(int id)
   {
       DataManager.GetData().dialogs[id] = 2;
       DataManager.Save_Auto();
   }*/
    // Start is called before the first frame update

}
