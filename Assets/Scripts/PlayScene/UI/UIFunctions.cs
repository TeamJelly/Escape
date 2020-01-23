using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class UIFunctions
{
    // Start is called before the first frame update

    public static void SelectScene(string name)
    {
        Time.timeScale = 1;
        LoadingManager.LoadScene(name);
        //데이터 로딩
    }
    public static void Save()
    {
        DataManager.Save();
    }

}
