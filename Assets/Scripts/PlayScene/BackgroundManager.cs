using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class BackgroundManager : MonoBehaviour
{
    public GameObject[] allNodes;
    public WorkNode currentNode;
    public Button interactButton;
    public Button BackButton;
    public static BackgroundManager instance;

    public GameObject notifyPanel;
    private void Awake()
    {
        instance = this;
        //allNodes = GameObject.FindGameObjectsWithTag("WorkNode");
        foreach (GameObject node in allNodes)
            node.GetComponent<WorkNode>().InitNode(interactButton);
        
        //currentNode.ShowNodes();
        currentNode.OnClick(currentNode);
    }
    //GUI상의 뒤로가기 버튼에 연결.
    public void EnableBackButton(AccessNode n)
    {
        BackButton.onClick.RemoveAllListeners();
        BackButton.onClick.AddListener(()=>
        {
            n.beforeNode.OnClick(n);
        });
        BackButton.gameObject.SetActive(true);
    }
    public void DisableBackButton()
    {
        BackButton.gameObject.SetActive(false);
    }
    public void EnableGetItemPannel(ItemNode n)
    {
        notifyPanel.GetComponentInChildren<Text>().text = n.item.itemName + " 을(를) 획득하였다.";
        notifyPanel.SetActive(true);
        //아이템 획득패널 활성화. 아이템 노드 내의 아이템 객체에서 이미지 뽑아서 보여주기.
    }
}
