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

    public void Enable(Quest q)
    {
        title.text = q.title;
        problem.text = q.problem;
        description.text = q.description;
        thisUI.SetActive(true);
    }
}
