using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDoor : Interactor
{
    public override void CallbackFunction(Slot slot)
    {
        gameObject.SetActive(false);
        ChatSystem2.instance.Monologue("문이 열렸다");
        DataManager.GetData().items[slot.itemID] = 2;
        Destroy(slot.gameObject);
    }

    public override bool CheckFinished()
    {
        return false;
    }
}
