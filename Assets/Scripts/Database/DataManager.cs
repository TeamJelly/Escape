﻿using System.Collections;
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
    public static PlayerData currentData = null;

    public static void StartAsNew()
    {
        currentData = new PlayerData();
        currentData.currentScene = "Intro";
        Save();
    }
    public static void Save()
    {
        currentData.currentScene = SceneManager.GetActiveScene().name;
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
        }
    }
     
}
[Serializable]
public class PlayerData
{
    public string currentScene = "PlayScene";

    public int[] items = new int[100]; 
    public int[] events = new int[100]; //0은 발견되지 않음; 1은 발견 및 수집된 상태; 2는 완료되었거나 소진된 상태.
    public PlayerData()
    {
        Array.Clear(items, 0, items.Length);
        Array.Clear(events, 0, events.Length);
        
    }
}