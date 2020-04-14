using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    public CanvasGroup thisUI;
    public Button RecentGameLoadButton;
    public Button NewGameButton;
    public Button SaveButton;
    public Button LoadButton;
    public Button OptionButton;
    public Button ExitButton;
    public Button BackButton;

    public GameObject DataPanel;
    public GameObject OptionPanel;
    
    private void Awake()
    {
        RecentGameLoadButton?.onClick.AddListener(() =>
        {
            DataManager.Load("0_AutoSave_최근플레이");
            PlayUIManager.instance.FadeOutForNextScene(DataManager.GetData().currentScene);
        });

        NewGameButton?.onClick.AddListener(() =>
        {
            PlayUIManager.instance.FadeOutForNextScene("Intro");
        });

        SaveButton?.onClick.AddListener(() => {
//            PlayUIManager.instance.FadeOut(() =>
//            {
                DataPanel.GetComponent<DataSelector>().SetSaveMode();
//            });
        });

        LoadButton?.onClick.AddListener(() =>
        {
//          PlayUIManager.instance.FadeOut(() =>
//            {
                DataPanel.GetComponent<DataSelector>().SetLoadMode();
//            });
        });

        OptionButton?.onClick.AddListener(() => { });

        ExitButton?.onClick.AddListener(() => { Application.Quit(); });

        BackButton?.onClick.AddListener(() => { DisableMenu(); });
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
