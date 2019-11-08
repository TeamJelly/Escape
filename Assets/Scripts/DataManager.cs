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
    public static PlayerData currentData;
    public static void AddSaveFile(PlayerData data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/" + data.name + ".sav", FileMode.Create);
        bf.Serialize(stream, data);
        stream.Close();

        bf = new BinaryFormatter();
        stream = new FileStream(Application.persistentDataPath + "/saveFileList.list", FileMode.Create);
        if(!saveFileList.Contains(data.name))
             saveFileList.Add(data.name);
        bf.Serialize(stream, saveFileList);
        stream.Close();
    }
    public static void DeleteSaveFile(string fileName)
    {
        File.Delete(Application.persistentDataPath + "/" + fileName + ".sav");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/saveFileList.list", FileMode.Create);
        saveFileList.Remove(fileName);
        bf.Serialize(stream, saveFileList);
        stream.Close();
        
    }
    public static PlayerData LoadSaveFile(string fileName)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/" + fileName + ".sav", FileMode.Open);
        PlayerData data = bf.Deserialize(stream) as PlayerData;
        stream.Close();
        //currentData = data;
        return data;

    }

    public static List<string> GetSaveFileList()
    {
        
        List<string> list;
        if (File.Exists(Application.persistentDataPath + "/saveFileList.list"))
        {
            Debug.Log(Application.persistentDataPath);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/saveFileList.list", FileMode.Open);

            list = bf.Deserialize(stream) as List<string>; 
            stream.Close();
        }
        else list = new List<string>();
        return list;
    }
}
[Serializable]
public class PlayerData
{
    public string name;
    int heart = 5;
    int time = 0;
    int eventPrograss = 0;
    //public List<Item> items = new List<Item>();
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
    }
}
