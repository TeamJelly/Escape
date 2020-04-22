using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public static class DataManager
{
    // Start is called before the first frame update
    public static PlayerData currentData;
    public static bool dataExist = false; // 메인화면에서 load 버튼 활성화 여부 판단.
  
    public static PlayerData GetData()
    {
        if (currentData == null)
        {
            Debug.Log("Data is null");
            InitDatabases();
//            currentData = new PlayerData();
//            currentData.currentScene = SceneManager.GetActiveScene().name;
        }
        return currentData;
    }

    public static Dictionary<string, bool> GetStates()
    {
        return GetData().states;
    }

    public static void SetState(string key, bool _bool)
    {
        GetData().states[key] = _bool;
        Save_Auto();
    }

    public static void StartAsNew()
    {
        currentData = null;
        InitDatabases();
    }

    public static void DeleteFile(string dataName)
    {
        Debug.Log(Application.persistentDataPath + "/" + dataName + ".sav");
        try { File.Delete(Application.persistentDataPath + "/" + dataName + ".sav"); }
        catch
        {
            Debug.Log("삭제안됨");
        }
    }

    public static void Save_Auto()
    {
        currentData.currentScene = SceneManager.GetActiveScene().name;
        Save("0_AutoSave_최근플레이");
    }

    public static void Save(string saveName)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/" + saveName + ".sav", FileMode.Create);
        bf.Serialize(stream, currentData);
        stream.Close();
        Debug.Log("Saved!");
    }

    public static bool Exists(string fileName)
    {
        return File.Exists(Application.persistentDataPath + "/" + fileName + ".sav");
    }

    public static void Load(string saveName)
    {
        if (File.Exists(Application.persistentDataPath + "/" + saveName + ".sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/" + saveName + ".sav", FileMode.Open);
            currentData = bf.Deserialize(stream) as PlayerData;
            stream.Close();
            dataExist = true;
        }
        else dataExist = false;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void InitDatabases()
    {
        currentData = new PlayerData();

        foreach (string key in PlayerData.statesKeys)
            currentData.states.Add(key, false);

        Debug.Log("Initialized finish");
    }

}
[Serializable]
public class PlayerData
{
    public string currentScene = "시작";
    public Dictionary<string, bool> states = new Dictionary<string, bool>();
    public static string[] statesKeys =
    {
            "인트로시작",
            "인트로다봄",

            "고양이획득",
            "고양이소모",

            "안작동손전등획득",
            "안작동손전등소모",

            "작동손전등획득",
            "작동손전등소모",

            "건전지획득",
            "건전지소모",

            "베개바꾸기퍼즐완료",
            "손전등조립퍼즐완료",
    };

    public PlayerData()
    {
        states.Clear();
    }
}
