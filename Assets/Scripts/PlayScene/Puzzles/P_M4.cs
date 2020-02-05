using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_M4 : Puzzle // 비밀번호 자물쇠 퍼즐
{
    
    public Button[] buttons; // 자물쇠 숫자들.
    public Button checkButton; // 열기 버튼
    public GameObject unlockMessage;
    int[] vals = new int[4]; // 자물쇠 현재상태.

    [SerializeField]
    public int[] answer = new int[4]; // 정답
    private void Awake()
    {
        eventID = 22;
    }
    private void Start()
    {
        System.Array.Clear(vals, 0, vals.Length);
        for (int i = 0; i < buttons.Length; i++)
        {
            int temp = i;
            buttons[i].onClick.AddListener(() => 
            {
                vals[temp] += 1;
                if (vals[temp] > 9) vals[temp] = 0;
                buttons[temp].GetComponentInChildren<Text>().text = vals[temp].ToString();
            }); // 각 숫자칸마다 터치해서 숫자증가 기능 추가.
        }

        checkButton.onClick.AddListener(CheckResult);
    }
    void CheckResult()
    {
        if (vals[0] == answer[0] && vals[1] == answer[1] && vals[2] == answer[2] && vals[3] == answer[3])
        {
            OnEnd();
        }           
    }
    public override void OnEnd()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.RemoveAllListeners();
        }
        unlockMessage.SetActive(true);
        QuestManager.instance.FinishQuest(eventID);
    }

}
