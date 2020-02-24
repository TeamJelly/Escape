using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Puzzle : MonoBehaviour
{
    public CanvasGroup thisUI;

    public UnityEvent OnEnable;
    public UnityEvent OnDisable;
    public UnityEvent OnEnd;
    
    public int puzzleID;
    public string puzzleName;
    //퍼즐 활성화 하기 전 이전에 퍼즐을 본 경험이 있는지 깼었는지 채크. 
    public void Start()
    {

        InitPuzzle();
        if (DataManager.GetData().puzzles[puzzleID] == 2)
        {
            OnDisable.Invoke();
        }
    }
    public abstract void InitPuzzle();
    public void EnablePuzzle()
    {
        PlayUIManager.instance.FadeIn(thisUI);
        if (DataManager.GetData().puzzles[puzzleID] == 2)
        {
            OnDisable.Invoke();
        }
        else OnEnable.Invoke();
    }

    public void DisablePuzzle()
    {
        if(thisUI != null)
         PlayUIManager.instance.FadeOut(thisUI);
        OnDisable.Invoke();
    }
    public void ExitPuzzle()
    {
        PlayUIManager.instance.FadeOut(thisUI);
    }
}
