using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemNode : WorkNode
{
    public Item item;
    //아이템 획득.
    public override void OnClick(WorkNode from)
    {
        PlayManager.instance.GetItem(item);
        BackgroundManager.instance.EnableGetItemPannel(this);
        button.gameObject.SetActive(false);
        from.nodes.Remove(this);
        Destroy(this.gameObject);
    }
}
