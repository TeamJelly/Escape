using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataBox : MonoBehaviour
{
    public Text dataNameAndTime;
    public Text dataIndex;
    public Button button;

    public int index = 0;

    public void Init(int i)
    {
        index = i;
        dataIndex.text = i.ToString();
        dataNameAndTime.text = "빈 슬롯";
    }
    public void ResetData()
    {
        button.onClick.RemoveAllListeners();
        dataNameAndTime.text = "빈 슬롯";
    }

    public void InitAsLoadBox(string fileName)
    {

        SetText(fileName);
        button.onClick.RemoveAllListeners();
        void act()
        {
            DataManager.Load(fileName);

            PlayUIManager.instance.FadeOutForNextScene(DataManager.GetData().currentScene);
            //DataSelector.instance.SetLoadMode();
        };
        button.onClick.AddListener(() =>
        {
            PlayUIManager.instance.NoticeDataWarning("데이터를 불러오면\n 최근 데이터는 날아갑니다.", act);
        });

    }
    public void InitAsSaveBox(string fileName)
    {
        SetText(fileName);

        void act()
        {
            DataManager.DeleteFile(fileName);
            DataManager.currentData.currentScene = SceneManager.GetActiveScene().name;
            string saveName = index + "_" + DataManager.currentData.currentScene + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            DataManager.Save(saveName);
            DataManager.Save_Auto();
            DataSelector.instance.SetSaveMode();
        }
        button.onClick.RemoveAllListeners();
        if (DataManager.Exists(fileName))
        {
            button.onClick.AddListener(() =>
            {
                PlayUIManager.instance.NoticeDataWarning("기존의 데이터 파일에 덮어쓰시겠습니까?", act);
            });
        }
        else button.onClick.AddListener(()=>act());
    }
    public void InitAsDeleteBox(string fileName)
    {
        SetText(fileName);
        button.onClick.RemoveAllListeners();
        void act()
        {
            DataManager.DeleteFile(fileName);
            DataSelector.instance.SetDeleteMode();
        };
        if (DataManager.Exists(fileName))
        {
            button.onClick.AddListener(() =>
            {
                PlayUIManager.instance.NoticeDataWarning("파일을 삭제하시겠습니까?", act);
            });
        }
    }
    public void SetText(string fileName)
    {
        string[] index_name_time = fileName.Split('_');
        dataNameAndTime.text = index_name_time[1];
        string[] index_time = index_name_time[2].Split('-');

        if (index_time.Length == 1)
            dataNameAndTime.text = index_name_time[1];
        else
            dataNameAndTime.text = index_name_time[1] + " / " + index_time[0] + "-" + index_time[1] + "-" + index_time[2] + " " + index_time[3] + ":" + index_time[4];
    }
}