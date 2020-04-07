﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionUIManager : MonoBehaviour
{
    public static SelectionUIManager instance;
    public GameObject SelectionPanel;
    public GameObject SelectionContent;
    public GameObject ButtonPrefab;
    //버튼을 만든다.
    //버튼에 기능을 추가한다.

    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }

    public void MakeButton(string text)
    {
        ButtonPrefab.SetActive(true);
        GameObject tempButton = Instantiate(ButtonPrefab, Vector3.one, Quaternion.identity, SelectionContent.transform);
        tempButton.GetComponent<Button>().onClick.AddListener(DeleteAllButton);
        tempButton.GetComponentInChildren<Text>().text = text;
        ButtonPrefab.SetActive(false);
    }

    public void DeleteAllButton()
    {
        Transform[] childList = SelectionContent.GetComponentsInChildren<Transform>(true);
        if (childList != null)
        {
            for (int i = 0; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                    Destroy(childList[i].gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
