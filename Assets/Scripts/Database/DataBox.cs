using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBox : MonoBehaviour
{
    public Text dataName;
    public Text dataTime;
    public Button button;
    public void InitAsLoadBox(string fileName)
    {
        string[] index_name_time = fileName.Split('_');
        dataName.text = index_name_time[0] + "." + index_name_time[1];
        dataTime.text = index_name_time[2];
        button.onClick.AddListener(() =>
        {
            DataManager.Load(fileName);
            PlayUIManager.instance.FadeOutForNextScene(DataManager.GetData().currentScene);
            //DataSelector.instance.SetLoadMode();
        });
    }
    public void InitAsSaveBox(string fileName)
    {
        string[] index_name_time = fileName.Split('_');
        dataName.text = index_name_time[0] + "." + index_name_time[1];
        dataTime.text = index_name_time[2];
        button.onClick.AddListener(() =>
        {
            DataManager.DeleteFile(fileName);
            DataManager.currentData.fileIndex = Int32.Parse(index_name_time[0]);
            string saveName = DataManager.currentData.dataName;
            string originName = DataManager.currentData.dataName_before;
            DataManager.currentData.dataName_before = saveName;

            DataManager.Save(saveName);
            if (originName == fileName)
                DataManager.currentData.dataName_before = saveName;

            //덮어쓰기를 한 후에는
            //어떤 데이터로 계속 갱신할 것인가.
            //기존의 데이터로 계속 하려면 -> 새로 만든 데이터 저장 후 기존 데이터 불러오기.
            else DataManager.Load(originName);
            //새로 만든 데이터로 계속하려면
            //DataManager.currentData.dataName_before = saveName;
            DataSelector.instance.SetSaveMode();
            Debug.Log("saveButton");
        });
    }
    public void InitAsDeleteBox(string fileName)
    {
        string[] index_name_time = fileName.Split('_');
        dataName.text = index_name_time[0] + "." + index_name_time[1];
        dataTime.text = index_name_time[2];
        button.onClick.AddListener(() =>
        {
            DataManager.DeleteFile(fileName);
            DataSelector.instance.SetDeleteMode();
        });
    }
}