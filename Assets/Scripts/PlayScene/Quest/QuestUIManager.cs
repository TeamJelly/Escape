using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUIManager : MonoBehaviour
{
    public GameObject thisUI;
    public Text title;
    public Text problem;
    public Text description;
    public GameObject todoPanel;
    public GameObject todoDescriptionPanel;
    int[] mainQList;
    int[] subQList;
    Button[] slots;
    public void Enable(Quest q)
    {
        title.text = q.title;
        problem.text = q.problem;
        description.text = q.description;
        thisUI.SetActive(true);
    }

    private void Awake()
    {
        slots = todoPanel.GetComponentsInChildren<Button>();
        mainQList = DataManager.currentData.mainEvents;
        subQList = DataManager.currentData.subEvents;
    }

    public void UpdateTodoList()
    {
        int slotIdx = 0;
        for (int i = 0; i < mainQList.Length; i++)
        {
            if (mainQList[i] == 0) continue;
            Quest q = QuestDatabase.GetQuestWithID("Main", i);
            slots[slotIdx].onClick.RemoveAllListeners();
            slots[slotIdx].onClick.AddListener(() => Enable(q));
            if (mainQList[i] == 1)
                slots[slotIdx].gameObject.GetComponentInChildren<Text>().text = q.title;
            else if(mainQList[i] == 2)
                slots[slotIdx].gameObject.GetComponentInChildren<Text>().text = q.title + "-완료";
            slotIdx++;
        }
        for (int i = 0; i < subQList.Length; i++)
        {
            if (subQList[i] == 0) continue;           
            Quest q = QuestDatabase.GetQuestWithID("Sub", i);
            if (subQList[i] == 1)
                slots[slotIdx].gameObject.GetComponentInChildren<Text>().text = QuestDatabase.GetQuestWithID("Sub", i).title;
            else if (subQList[i] == 2)
                slots[slotIdx].gameObject.GetComponentInChildren<Text>().text = QuestDatabase.GetQuestWithID("Sub", i).title + "-완료";
            slotIdx++;
        }
    }
}
