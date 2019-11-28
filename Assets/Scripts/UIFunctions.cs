using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIFunctions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SelectScene(string name)
    {
        Time.timeScale = 1;
        LoadingManager.LoadScene(name);
        //데이터 로딩
    }
    public void NextEvent(string name)
    {
        DataManager.currentData.currentScene = name;
        Save();
        SelectScene(name);
    }
    public void ToggleUI(GameObject ui)
    {
        ui.SetActive(!ui.activeSelf);
    }

    public void Load()
    {
        DataManager.Load();
    }
    public void Save()
    {
        DataManager.Save();
    }
    public void StartNew()
    {
        DataManager.StartAsNew();
        SelectScene("Intro");
    }
}
