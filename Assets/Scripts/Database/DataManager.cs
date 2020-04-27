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
    public string currentScene = "침실";
    public Dictionary<string, bool> states = new Dictionary<string, bool>();
    public static string[] statesKeys =
    {
            "           [인트로 관련]",
            "인트로시작",
            "인트로다봄",

            "           [아이템 획득]",
            "작동손전등획득",
            "맥가이버획득",
            "고양이획득",
            "안작동손전등획득",
            "건전지획득",
            "서랍열쇠획득",
            "신용카드획득",
            "녹슨열쇠획득",
            "연이방열쇠획득",
            "부러진열쇠획득",
            "복제한열쇠획득",
            "밀가루획득",
            "치약획득",
            "테이프획득",
            "네임펜획득",


            "           [아이템 소모]",
            "작동손전등소모",
            "맥가이버소모",
            "고양이소모",
            "안작동손전등소모",
            "건전지소모",
            "서랍열쇠소모",
            "신용카드소모",
            "녹슨열쇠소모",
            "연이방열쇠소모",
            "부러진열쇠소모",
            "복제한열쇠소모",
            "밀가루소모",
            "치약소모",
            "테이프소모",
            "네임펜소모",

            "           [손전등 상태]",
            "손전등킴",

            "           [맥가이버 상태]",
            "맥가이버스패너",
            "맥가이버드라이버",
            "맥가이버나이프",

            "           [방입장]",
            "거실최초입장",
            "화장실최초입장",
            "다용도실최초입장",
            "연이방최초입장",
            "부엌최초입장",

            "           [퍼즐]",
            "베개바꾸기완료",
            "손전등조립완료",
            "연이방문해제완료",
            "현관문열쇠해제완료",
            "현관문번호해제완료",
            "현관문지문해제완료",
    };

    public PlayerData()
    {
        states.Clear();
    }
}
