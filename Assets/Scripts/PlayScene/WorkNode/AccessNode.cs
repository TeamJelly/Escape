using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessNode : WorkNode
{

    // Start is called before the first frame update
    public bool canBack;
    public override void OnClick(WorkNode from)
    {
        from.OnExit();
        foreach (WorkNode node in nodes)
        {
            node.beforeNode = this;
            node.button.onClick.RemoveAllListeners();
            //순서 유의!
            //node.button.onClick.AddListener(OnExit);
            node.button.onClick.AddListener(() => node.OnClick(this));           
        }
        ShowNodes();
        if (canBack)
            BackgroundManager.instance.EnableBackButton(this);
       
    }
}
