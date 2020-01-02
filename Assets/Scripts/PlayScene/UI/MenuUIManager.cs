﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    GameObject currentPanel;
    public Button inventoryTab;
    public Button todoTab;
    public Button optionTab;
    public Button mainTab;

    public GameObject inventoryPanel;
    public GameObject todoPanel;
    public GameObject optionPanel;

    private void Awake()
    {
        currentPanel = inventoryPanel;
        currentPanel.SetActive(true);
        inventoryTab.onClick.AddListener(() => SwabPanel(inventoryPanel));
        todoTab.onClick.AddListener(() => SwabPanel(todoPanel));
        optionTab.onClick.AddListener(() => SwabPanel(optionPanel));
        mainTab.onClick.AddListener(() => { this.gameObject.SetActive(false); PlayUIManager.instance.FadeOutForNextScene("MainScene"); });//UIFunctions.SelectScene("MainScene"));
    }

    void SwabPanel(GameObject panel)
    {
        currentPanel.SetActive(false);
        currentPanel = panel;
        currentPanel.SetActive(true);
    }

}
