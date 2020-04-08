using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;


public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public GameObject SaveListPanel;
    public GameObject DataSlotPrefab;

    string[] saveFileList;
    List<GameObject> saveList = new List<GameObject>();

    private void Awake()
    {
        instance = this;

        for(int i = 0; i < 10; i++)
        {
            GameObject newData = Instantiate(DataSlotPrefab,Vector2.one,Quaternion.identity,SaveListPanel.transform);
            newData.GetComponent<DataBox>().Init(i + 1);
            saveList.Add(newData);
        }
    }

    void ResetDataBoxies()
    {
        foreach (GameObject box in saveList)
            box.GetComponent<DataBox>().ResetData();
    }


}
