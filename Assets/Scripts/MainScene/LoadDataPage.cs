using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadDataPage : MonoBehaviour
{
    // Start is called before the first frame update
    public Button[] loadButtons;
    public GameObject Dialog;


    private void Awake()
    {
        
    }
    public void Test()
    {
        DataManager.saveFileList = DataManager.GetSaveFileList();
        PlayerData p = new PlayerData("data 1");
        p.heart = 1;
        DataManager.AddSaveFile(p);
        p = new PlayerData("data 2");
        p.heart = 2;
        DataManager.AddSaveFile(p);
        p = new PlayerData("data 3");
        p.heart = 3;
        DataManager.AddSaveFile(p);
    }
    // Update is called once per frame
    public void LoadDataList()
    {
       
        Dialog.SetActive(false);
        for (int i = 0; i < loadButtons.Length; i++)
        {
            loadButtons[i].onClick.RemoveAllListeners();
            loadButtons[i].gameObject.SetActive(false);
        }
       DataManager.saveFileList = DataManager.GetSaveFileList();
        for (int i = 0; i <  DataManager.saveFileList.Count; i++)
        {
            
            loadButtons[i].gameObject.SetActive(true);
            int temp = i;
            loadButtons[i].onClick.AddListener(()=>SelectData(DataManager.saveFileList[temp]));
            loadButtons[i].onClick.AddListener(()=>Dialog.SetActive(true));
            loadButtons[i].GetComponentInChildren<Text>().text = DataManager.saveFileList[i];
        }
    }

    PlayerData selected;
    
    public void LoadData()
    {
        DataManager.currentData = selected;
    }
    
    public void SelectData(string s)
    {
        selected = DataManager.LoadSaveFile(s);
    }
    public void DeleteData()
    {
        DataManager.DeleteSaveFile(selected.name);
        Dialog.SetActive(false);
        LoadDataList();
    }
    public void AddData()
    {
        DataManager.AddSaveFile(DataManager.currentData);
    }
}
