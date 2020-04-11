using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    // GameObject currentPanel;
    public CanvasGroup thisUI;
    public Button SaveButton;
    public Button LoadButton;
//    public Button todoTab;
    public Button OptionButton;
    public Button ExitButton;
    public Button BackButton;

    public GameObject DataPanel;
    public GameObject OptionPanel;
    
    private void Awake()
    {
        SaveButton.onClick.AddListener(() => { DataPanel.GetComponent<DataSelector>().SetSaveMode(); });
        LoadButton.onClick.AddListener(() => { DataPanel.GetComponent<DataSelector>().SetLoadMode(); });
//        todoTab.onClick.AddListener(() => { currentTab = todoTab; SwabPanel(questPanel); });
        OptionButton.onClick.AddListener(() => { });
        ExitButton.onClick.AddListener(() => { Application.Quit(); });
        BackButton.onClick.AddListener(() => { DisableMenu(); });//UIFunctions.SelectScene("MainScene"));
    }

    public void EnableMenu()
    {
        if (thisUI.isActiveAndEnabled == false)
        {
            BackgroundManager.instance.isPaused = true;
            PlayUIManager.instance.FadeIn(thisUI);
        }
    }

    public void DisableMenu()
    {
        BackgroundManager.instance.isPaused = false;
        PlayUIManager.instance.FadeOut(thisUI);
    }

}
