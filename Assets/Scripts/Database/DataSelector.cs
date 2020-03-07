using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class DataSelector : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dataBoxListObj;
    public GameObject dataBoxPrefeb;
    public Button addNewDataButton;
    string[] saveFileList;
    List<GameObject> boxies = new List<GameObject>();

    public static DataSelector instance;
    private void Awake()
    {
        instance = this;

        for(int i = 0; i < 10; i++)
        {
            GameObject newBox = GameObject.Instantiate(dataBoxPrefeb);
            newBox.GetComponent<DataBox>().Init(i+1);
            AddToList(newBox);
        }
    }
    void ResetDataBoxies()
    {
        foreach(GameObject box in boxies)
            box.GetComponent<DataBox>().ResetData();
    }
    void AddToList(GameObject newBox)
    {
        newBox.transform.SetParent(dataBoxListObj.transform);
        RectTransform rect = (RectTransform)newBox.transform;
        rect.localScale = Vector2.one;
        boxies.Add(newBox);
    }
    public void SetLoadMode()
    {
        ResetDataBoxies();
        saveFileList = Directory.GetFiles(Application.persistentDataPath, "*.sav");
        for(int i = 1; i < saveFileList.Length; i++)
        {
            string fileName = Path.GetFileNameWithoutExtension(saveFileList[i]);
            int fileIndex = int.Parse(fileName.Split('_')[0]) - 1;
            if(fileIndex < boxies.Count)
            {
                boxies[fileIndex].GetComponent<DataBox>().InitAsLoadBox(fileName);
            }
        }
    }
    public void SetSaveMode()
    {
        ResetDataBoxies();
        for(int i = 0; i < 10; i++)
        {
            boxies[i].GetComponent<DataBox>().InitAsSaveBox("_빈 데이터_");
        }
        saveFileList = Directory.GetFiles(Application.persistentDataPath, "*.sav");
        for (int i = 1; i < saveFileList.Length; i++)
        {
            string fileName = Path.GetFileNameWithoutExtension(saveFileList[i]);
            int fileIndex = int.Parse(fileName.Split('_')[0]) - 1;
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
