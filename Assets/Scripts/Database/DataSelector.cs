using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class DataSelector : MonoBehaviour
{
    public static DataSelector instance;

    // Start is called before the first frame update
    public GameObject dataBoxListPanel;
    public GameObject dataBoxPrefeb;
    //    public Button addNewDataButton;

    public GameObject SaveTitle;
    public GameObject LoadTitle;

    public Button BackButton;

    string[] saveFileList;
    List<GameObject> boxies = new List<GameObject>();

    private void Awake()
    {
        instance = this;
        BackButton.onClick.AddListener(() => { PlayUIManager.instance.FadeOut(GetComponent<CanvasGroup>());});

        for(int i = 0; i < 10; i++)
        {
            GameObject newBox = GameObject.Instantiate(dataBoxPrefeb);
            newBox.GetComponent<DataBox>().Init(i);
            AddToList(newBox);
        }

        string autoFileName = "0_AutoSave_최근플레이";
        boxies[0].GetComponent<DataBox>().InitAsLoadBox(autoFileName);
    }

    private void OnEnable()
    {
        PlayUIManager.instance.FadeIn(GetComponent<CanvasGroup>());
    }

    void ResetDataBoxies()
    {
        for (int i = 1; i < 10; i++)
            boxies[i].GetComponent<DataBox>().ResetData();
    }

    void AddToList(GameObject newBox)
    {
        newBox.transform.SetParent(dataBoxListPanel.transform);
        RectTransform rect = (RectTransform)newBox.transform;
        rect.localScale = Vector2.one;
        boxies.Add(newBox);
    }

    public void SetLoadMode()
    {
        gameObject.SetActive(true);
        SaveTitle.SetActive(false);
        LoadTitle.SetActive(true);

        ResetDataBoxies();

        saveFileList = Directory.GetFiles(Application.persistentDataPath, "*.sav");


        if (saveFileList.Length != 0 && Path.GetFileNameWithoutExtension(saveFileList[0]) == "0_AutoSave_최근플레이") 
            boxies[0].SetActive(true);
        else
            boxies[0].SetActive(false);

        for (int i = 1; i < saveFileList.Length; i++)
        {
            string fileName = Path.GetFileNameWithoutExtension(saveFileList[i]);
            int fileIndex = int.Parse(fileName.Split('_')[0]);
            if(fileIndex < boxies.Count)
            {
                boxies[fileIndex].GetComponent<DataBox>().InitAsLoadBox(fileName);
            }
        }
    }

    public void SetSaveMode()
    {
        gameObject.SetActive(true);
        SaveTitle.SetActive(true);
        LoadTitle.SetActive(false);

        ResetDataBoxies();

        for (int i = 1; i < 10; i++)
        {
            boxies[i].GetComponent<DataBox>().InitAsSaveBox("_빈 슬롯_");
        }

        boxies[0].SetActive(false);

        saveFileList = Directory.GetFiles(Application.persistentDataPath, "*.sav");
        for (int i = 1; i < saveFileList.Length; i++)
        {
            string fileName = Path.GetFileNameWithoutExtension(saveFileList[i]);
            int fileIndex = int.Parse(fileName.Split('_')[0]);
            if (fileIndex < boxies.Count)
            {
                boxies[fileIndex].GetComponent<DataBox>().InitAsSaveBox(fileName);
            }
        }
    }

    public void SetDeleteMode()
    {
        ResetDataBoxies();
        saveFileList = Directory.GetFiles(Application.persistentDataPath, "*.sav");
        for (int i = 1; i < saveFileList.Length; i++)
        {
            string fileName = Path.GetFileNameWithoutExtension(saveFileList[i]);
            int fileIndex = int.Parse(fileName.Split('_')[0]) - 1;
            if (fileIndex < boxies.Count)
            {
                boxies[fileIndex].GetComponent<DataBox>().InitAsDeleteBox(fileName);
            }
        }
    }

    public void LoadAutoSave(CanvasGroup notifyPanel)
    {
        DataManager.Load("0_AutoSave_최근플레이");
        if(DataManager.currentData == null)
        {
            PlayUIManager.instance.FadeIn(notifyPanel);
        }
        else PlayUIManager.instance.FadeOutForNextScene(DataManager.GetData().currentScene);
    }

    public void StartNew(CanvasGroup notifyPanel)
    {
        DataManager.Load("0_AutoSave_최근플레이");
        if (DataManager.currentData == null)
        {
            DataManager.StartAsNew();
            PlayUIManager.instance.FadeOutForNextScene("Intro");
        }
        else PlayUIManager.instance.FadeIn(notifyPanel);
    }

    public void StartNew()
    {
        DataManager.StartAsNew();
        PlayUIManager.instance.FadeOutForNextScene("Intro");
    }
}