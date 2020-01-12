using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class UIFunctions
{
    public static GameObject currentUI;
    // Start is called before the first frame update

    public static void SwabCurrentUI(GameObject g)
    {
        currentUI.SetActive(false);
        currentUI = g;
        currentUI.SetActive(true);
    }
    public static void SelectScene(string name)
    {
        Time.timeScale = 1;
        LoadingManager.LoadScene(name);
        //데이터 로딩
    }
    public static void NextEvent(string name)
    {
        DataManager.currentData.currentScene = name;
        Save();
        SelectScene(name);
    }
    public static void ToggleUI(GameObject ui)
    {
        ui.SetActive(!ui.activeSelf);
    }

    public static void Save()
    {
        DataManager.Save();
    }
     
}
