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
            PlayUIManager.instance.NoticeDataWarning("새로 시작하시겠습니까?\n자동저장된 데이터는 사라집니다.", () => {
                DataManager.StartAsNew();
                PlayUIManager.instance.FadeOutForNextScene("침실");
            });
        });

        SaveButton?.onClick.AddListener(() => {
            DataPanel.GetComponent<DataSelector>().SetSaveMode();
        });

        LoadButton?.onClick.AddListener(() =>
        {
            DataPanel.GetComponent<DataSelector>().SetLoadMode();
        });

        OptionButton?.onClick.AddListener(() => { });

        ExitButton?.onClick.AddListener(() => {
            //Application.Quit(); 
            PlayUIManager.instance.FadeOutForNextScene("메인");
        });

        BackButton?.onClick.AddListener(() => { PlayUIManager.instance.FadeOut(thisUI); });
    }
}
