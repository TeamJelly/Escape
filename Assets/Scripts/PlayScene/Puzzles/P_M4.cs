using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_M4 : Puzzle // 비밀번호 자물쇠 퍼즐
{
    
    public Button[] buttons; // 자물쇠 숫자들.
    public int currentIndex; // 현재 선택된 자물쇠 칸
    public GameObject currentIndicator; // 그냥 선택된거 보여주는 테두리
    public Button checkButton; // 열기 버튼
    public Button AddButton; // 자물쇠 숫자 증가시키기
    public Button SubButton; // 자물쇠 숫자 감소시키기
    public GameObject unlockMessage;
    int[] vals = new int[4]; // 자물쇠 현재상태.

    [SerializeField]
    public int[] answer = new int[4]; // 정답
    private void Awake()
    {
        isMain = true; // 메인이벤트 ㅇㅇ
        eventID = 22;
    }
    private void Start()
    {
        System.Array.Clear(vals, 0, vals.Length);
        for (int i = 0; i < buttons.Length; i++)
        {
            int temp = i;
            buttons[i].onClick.AddListener(() => { SetCurrent(temp); }); // 각 숫자칸마다 터치해서 선택하는기능 추가.
        }
        AddButton.onClick.AddListener(() => { SetValue(1); }); // 숫자증가로직
        SubButton.onClick.AddListener(() => { SetValue(-1); }); // 숫자감소로직
        checkButton.onClick.AddListener(CheckResult);
    }

    //그냥 현재 선택된 다이얼 표시기 표시기능.
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
        if (vals[0] == answer[0] && vals[1] == answer[1] && vals[2] == answer[2] && vals[3] == answer[3])
        {
            //isCleared = true;
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
        DataManager.currentData.mainEvents[eventID] = 2;
        DataManager.Save();
    }

}
