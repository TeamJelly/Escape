using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIFunctions : MonoBehaviour
{
    public GameObject currentUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SwabCurrentUI(GameObject g)
    {
        currentUI.SetActive(false);
        currentUI = g;
        currentUI.SetActive(true);
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

    public void Save()
    {
        DataManager.Save();
    }
     
}
