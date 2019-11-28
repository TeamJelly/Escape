using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_M4 : Puzzle
{
    
    public Button[] buttons;
    public int currentIndex;
    public GameObject currentIndicator;
    public Button checkButton;
    public Button AddButton;
    public Button SubButton;
    public GameObject unlockMessage;
    int[] vals = new int[4];

    private void Awake()
    {
        isMain = true;
        eventID = 22;
    }
    private void Start()
    {
        System.Array.Clear(vals, 0, vals.Length);
        for (int i = 0; i < buttons.Length; i++)
        {
            int temp = i;
            buttons[i].onClick.AddListener(() => { SetCurrent(temp); });
        }
        AddButton.onClick.AddListener(() => { SetValue(1); });
        SubButton.onClick.AddListener(() => { SetValue(-1); });
        checkButton.onClick.AddListener(CheckResult);
    }

    void SetCurrent(int i)
    {
        Debug.Log(i);
        currentIndex = i;
        currentIndicator.transform.position = buttons[i].transform.position;
        currentIndicator.SetActive(true);
    }

    void SetValue(int i)
    {
        vals[currentIndex] = (vals[currentIndex] + i);
        if (vals[currentIndex] > 9) vals[currentIndex] = 0;
        else if (vals[currentIndex] < 0) vals[currentIndex] = 9;
        buttons[currentIndex].GetComponentInChildren<Text>().text = vals[currentIndex].ToString();
    }

    void CheckResult()
    {
        if (vals[0] == 1 && vals[1] == 2 && vals[2] == 2 && vals[3] == 4)
        {
            isCleared = true;
            OnEnd();
        }           
    }
    public override void OnEnd()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.RemoveAllListeners();
        }
        AddButton.gameObject.SetActive(false);
        SubButton.gameObject.SetActive(false);
        currentIndicator.SetActive(false);
        unlockMessage.SetActive(true);
    }

}
