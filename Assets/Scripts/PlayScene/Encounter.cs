using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    public int minimumTime = 3;
    public int maximumTime = 10;

    public string stateString;
    bool isDoorOpened = false;
    public void Awake()
    {
        //Debug.Log(StateDatabase.GetStateValue(stateString));
        if (StateDatabase.GetStateValue(stateString) == 1)
        {
            StartWork();
        }
    }

    public void StartWork()
    {
        StartCoroutine(Working());
    }
    IEnumerator Working()
    {
        int randNum = Random.Range(minimumTime, maximumTime);
        float t = 0;
        var waitReturn = new WaitWhile(() => BackgroundManager.instance.isPaused == true);
        while(t < randNum && !isDoorOpened)
        {
            t += Time.deltaTime;
            if (BackgroundManager.instance.isPaused)
                yield return waitReturn;
            yield return null;
        }
        StateManager.instance.EndState(stateString);
        ChatSystem2.instance.Monologue("연이가 나타났다!");
    }



    //이동하기 전 연이 밖에 있는지 확인하는 메소드.
    public void MoveScene(string name)
    {
        if (StateDatabase.GetStateValue(stateString) == 1)
        {
            isDoorOpened = true;
        }
        else PlayUIManager.instance.FadeOutForNextScene(name);
    }

}