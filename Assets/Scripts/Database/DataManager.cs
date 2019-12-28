using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public static class DataManager
{
    public static List<string> saveFileList;
    // Start is called before the first frame update
    public static PlayerData currentData = null;


    //단일저장시 사용.
    public static void StartAsNew()
    {
        currentData = new PlayerData("blank");
        currentData.currentScene = "Intro";
        Save();

    }
    public static void Save()
    {
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
    public string name;
    int heart = 5;
    int time = 0;
    int eventPrograss = 0;
    public string currentScene = "PlayScene";
    //public List<int> items = new List<int>();

    //0은 발견되지 않음; 1은 발견 및 수집된 상태; 2는 완료되었거나 소진된 상태.
    public int[] items = new int[100];
    public int[] mainEvents = new int[100];// 10 ~ 19 : 첫번째 이벤트 흐름의 이벤트들. 
    public int[] subEvents = new int[100];

    public int Heart
    {
        get { return heart; }
        set { heart = value > 10 ? 10 : value < 0 ? 0 : value; }
    }  
    public int Time
    {
        get { return time; }
        set { time = value > 10 ? 10 : value < 0 ? 0 : value; }
    }   
    public int EventPrograss
    {
        get { return eventPrograss; }
        set { eventPrograss = value > 10 ? 10 : value < 0 ? 0 : value; }
    }
    public PlayerData(string n)
    {
        name = n;
        Array.Clear(items, 0, items.Length);
        Array.Clear(mainEvents, 0, mainEvents.Length);
        Array.Clear(subEvents, 0, subEvents.Length);
        
    }
}
