using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataBox : MonoBehaviour
{
    public Text dataName;
    public Text dataTime;
    public Text dataIndex;
    public Button button;

    public int index = 0;

    public void Init(int i)
    {
        index = i;
        dataIndex.text = i.ToString();
        dataName.text = "빈 데이터";
        dataTime.text = "";
    }
    public void ResetData()
    {
        button.onClick.RemoveAllListeners();
        dataName.text = "빈 데이터";
        dataTime.text = "";
    }

    public void InitAsLoadBox(string fileName)
    {

        SetText(fileName);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            DataManager.Load(fileName);
            PlayUIManager.instance.FadeOutForNextScene(DataManager.GetData().currentScene);
            //DataSelector.instance.SetLoadMode();
        });
    }
    public void InitAsSaveBox(string fileName)
    {
        SetText(fileName);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            DataManager.DeleteFile(fileName);
            DataManager.currentData.currentScene = SceneManager.GetActiveScene().name;
            string saveName = index + "_" + DataManager.currentData.currentScene + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            DataManager.Save(saveName);
            DataManager.Save_Auto();
            DataSelector.instance.SetSaveMode();
            Debug.Log("saveButton");
        });
    }
    public void InitAsDeleteBox(string fileName)
    {
        SetText(fileName);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            DataManager.DeleteFile(fileName);
            DataSelector.instance.SetDeleteMode();
        });
    }
    public void SetText(string fileName)
    {
        string[] index_name_time = fileName.Split('_');
        dataName.text = index_name_time[1];
        dataTime.text = index_name_time[2];
    }
}