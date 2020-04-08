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

    public GameObject SavePanel;
    public GameObject LoadPanel;
    public GameObject OptionPanel;

    GameObject current;
    
    private void Awake()
    {
        SaveButton.onClick.AddListener(() => { SavePanel.SetActive(true); DataSelector.instance.SetSaveMode(); });
        LoadButton.onClick.AddListener(() => { LoadPanel.SetActive(true); DataSelector.instance.SetLoadMode(); });
//        todoTab.onClick.AddListener(() => { currentTab = todoTab; SwabPanel(questPanel); });
        OptionButton.onClick.AddListener(() => { });
        ExitButton.onClick.AddListener(() => { Application.Quit(); });
        BackButton.onClick.AddListener(() => { DisableMenu(); });//UIFunctions.SelectScene("MainScene"));
    }
    public void Start()
    {
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
