using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_M3 : Puzzle
{
    public Button useItemButton;
    public GameObject unlockMessage;

    public int neededItemID = 1;
    private void Awake()
    {
        isMain = true;
        eventID = 21;
        //useItemButton.onClick.AddListener(OnClickUseItem);
    }

    public void OnClickUseItem()
    {
        if(DataManager.currentData.items[neededItemID] == 1)
        {
            OnEnd();
        }
    }
    public override void OnEnd()
    {
        //isCleared = true;
        unlockMessage.SetActive(true);
        useItemButton.gameObject.SetActive(false);
        DataManager.currentData.mainEvents[eventID] = 2;
        DataManager.Save();
    }
}
