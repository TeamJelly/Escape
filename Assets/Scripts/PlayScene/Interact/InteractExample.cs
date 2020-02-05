using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractExample : Interactor
{
    public int rewardItemID = 0;
    public override void CallbackFunction(Slot slot)
    {
        Inventory.instance.AddItem(rewardItemID);
        this.gameObject.SetActive(false);
        Destroy(slot.gameObject);
    }

    public override bool CheckFinished()
    {
        return DataManager.GetData().items[rewardItemID] > 0;
    }
    public override void OnClickFunction()
    {
        
    }
}
