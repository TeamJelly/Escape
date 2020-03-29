using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    // GameObject currentPanel;
    public CanvasGroup thisUI;
    public Button SaveTab;
    public Button LoadTab;
    public Button todoTab;
    public Button optionTab;
    public Button mainTab;

    Button currentTab = null; 
    public GameObject SavenLoadPanel;
    public GameObject questPanel;
    public GameObject optionPanel;

    GameObject currentPanel;
    
    private void Awake()
    {
        currentPanel = SavenLoadPanel;
        currentPanel.SetActive(true);
        currentTab = SaveTab;
        SaveTab.onClick.AddListener(() => { currentTab = SaveTab; SwabPanel(SavenLoadPanel); DataSelector.instance.SetSaveMode(); });
        LoadTab.onClick.AddListener(() => { currentTab = LoadTab; SwabPanel(SavenLoadPanel); DataSelector.instance.SetLoadMode(); });
        todoTab.onClick.AddListener(() => { currentTab = todoTab; SwabPanel(questPanel); });
        optionTab.onClick.AddListener(() => { currentTab = optionTab; SwabPanel(optionPanel); });
        mainTab.onClick.AddListener(() => { currentTab = mainTab; PlayUIManager.instance.FadeOutForNextScene("MainScene"); });//UIFunctions.SelectScene("MainScene"));

        
    }
    public void Start()
    {
        SaveTab.onClick.Invoke();
    }
    public void EnableMenu()
    {
        BackgroundManager.instance.isPaused = true;
        PlayUIManager.instance.FadeIn(thisUI);
        currentTab.onClick.Invoke();
    }
    public void DisableMenu()
    {
        BackgroundManager.instance.isPaused = false;
        PlayUIManager.instance.FadeOut(thisUI);
    }
    void SwabPanel(GameObject panel)
    {
        currentPanel.SetActive(false);
        currentPanel = panel;
        currentPanel.SetActive(true);
    }

}
