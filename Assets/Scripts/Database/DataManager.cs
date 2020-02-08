using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public static class DataManager
{
    public static List<string> saveFileList;
    // Start is called before the first frame update
    static PlayerData currentData = null;
    public static bool dataExist = false;

    public static PlayerData GetData()
    {
        if (currentData == null)
        {
            QuestDatabase.InitQuestLists();
            ItemDatabase.InitItemList();
            SpeechBaloonManager.InitDialogList();
            currentData = new PlayerData();
            currentData.currentScene = SceneManager.GetActiveScene().name;
        }
        return currentData;
    }
    public static void StartAsNew()
    {
        currentData = new PlayerData();
        currentData.currentScene = "Intro";
        Save();
    }
    public static void Save()
    {
        
        GetData().currentScene = SceneManager.GetActiveScene().name;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/SaveFile.sav", FileMode.Create);
        bf.Serialize(stream, currentData);
        stream.Close();
    }
    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveFile.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/SaveFile.sav", FileMode.Open);
            currentData = bf.Deserialize(stream) as PlayerData;
            stream.Close();
            dataExist = true;
        }
        else dataExist = false;
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
    public PlayerData()
    {
        Array.Clear(items, 0, items.Length);
        Array.Clear(dialogs, 0, dialogs.Length);
        Array.Clear(events, 0, events.Length);
        Array.Clear(puzzles, 0, puzzles.Length);

        dialogs[0] = 1;
        
    }
}
