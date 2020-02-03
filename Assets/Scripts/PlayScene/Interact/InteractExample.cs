using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractExample : Interactor
{
    public override void CallbackFunction()
    {
        BackgroundManager.instance.GetItem(rewardItemID);
        this.gameObject.SetActive(false);
    }
}
