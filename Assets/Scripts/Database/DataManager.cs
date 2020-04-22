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
            currentData = new PlayerData();
            //int fileCount = Directory.GetFiles(Application.persistentDataPath, "*.sav").Length;
            currentData.currentScene = SceneManager.GetActiveScene().name;
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
        currentData = new PlayerData();
        currentData.currentScene = "Intro";
        //Save(currentData.dataName);
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

    //최초 1회만 실행.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void InitDatabases()
    {
        GetData();

        foreach (string key in PlayerData.statesKeys)
            currentData.states.Add(key, false);

        Debug.Log("Initialized finish");
    }

}
[Serializable]
public class PlayerData
{
    public string currentScene = "Intro";
    public int[] items = new int[100]; 
    public int[] puzzles = new int[100]; 
    public int[] dialogs = new int[100]; 
    public int[] events = new int[100]; //0은 발견되지 않음; 1은 발견 및 수집된 상태; 2는 완료되었거나 소진된 상태.
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
        Array.Clear(items, 0, items.Length);
        Array.Clear(dialogs, 0, dialogs.Length);
        Array.Clear(events, 0, events.Length);
        Array.Clear(puzzles, 0, puzzles.Length);
        states.Clear();

        dialogs[0] = 1;
        
    }
}
