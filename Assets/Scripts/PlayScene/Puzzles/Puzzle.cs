﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Puzzle : MonoBehaviour
{
    public string PuzzleName;
//    public bool SetInactiveOnComplete;

    public void Awake()
    {
        Debug.Log(" \"" + PuzzleName + "\" 퍼즐 활성화");
        if (PuzzleName != "" && DataManager.GetData().states[PuzzleName + "완료"])
            gameObject.SetActive(false);
        else
            InitPuzzle();
    }
    public abstract void InitPuzzle(); //퍼즐에 필요한 코드 초기화

    public void CompletePuzzle()
    {
        if (PuzzleName != "")
        {
            DataManager.GetData().states[PuzzleName + "완료"] = true;
            DataManager.Save_Auto();
            PlayUIManager.instance.PopUpBackButton.onClick.Invoke();
        }
//        if (!SetInactiveOnComplete)
//            gameObject.SetActive(false);
    }
}
