using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
   // GameObject currentPanel;
    public Button miniMapTab;
    public Button todoTab;
    public Button optionTab;
    public Button mainTab;

    public GameObject miniMapPanel;
    public GameObject todoPanel;
    public GameObject optionPanel;

    GameObject currentPanel;
    private void Awake()
    {
        currentPanel = miniMapPanel;
        currentPanel.SetActive(true);

        miniMapTab.onClick.AddListener(() => SwabPanel(miniMapPanel));
        todoTab.onClick.AddListener(() => SwabPanel(todoPanel));
        optionTab.onClick.AddListener(() => SwabPanel(optionPanel));
        mainTab.onClick.AddListener(() => PlayUIManager.instance.FadeOutForNextScene("MainScene"));//UIFunctions.SelectScene("MainScene"));
    }

    void SwabPanel(GameObject panel)
    {
        currentPanel.SetActive(false);
        currentPanel = panel;
        currentPanel.SetActive(true);
    }

}
