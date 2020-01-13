using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUIManager : MonoBehaviour
{
    public static QuestUIManager instance;
    public GameObject thisUI;
    public Text title;
    public Text problem;
    public Text description;
    public GameObject todoPanel;
   // public GameObject todoDescriptionPanel;
    Button[] slots;
    int[] events;


    private void Awake()
    {
        events = DataManager.currentData.events;
        instance = this;
        slots = todoPanel.GetComponentsInChildren<Button>();
    }
    public void Enable(Quest q)
    {
        title.text = q.title;
        problem.text = q.problem;
        description.text = q.description;
        thisUI.SetActive(true);
    }

    public void UpdateTodoList()
    {
        int slotIdx = 0;
        for (int i = 0; i < 100; i++)
        {
            if(events[i] == 0) continue;
            Quest q = QuestDatabase.GetQuestWithID(i);
            
            slots[slotIdx].onClick.RemoveAllListeners();
            slots[slotIdx].onClick.AddListener(() => Enable(q));
            if (DataManager.currentData.events[q.ID] == 1)
                slots[slotIdx].gameObject.GetComponentInChildren<Text>().text = q.title;
            else if(DataManager.currentData.events[i] == 2)
                slots[slotIdx].gameObject.GetComponentInChildren<Text>().text = q.title + "-완료";
            slotIdx++;
        }
    }
}
