using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class DataSelector : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dataBoxListObj;
    public GameObject dataBoxPrefeb;
    public Button addNewDataButton;
    string[] saveFileList;
    List<GameObject> boxies = new List<GameObject>();

    public static DataSelector instance;
    private void Awake()
    {
        instance = this;
    }
    public void SetLoadMode()
    {
        saveFileList = Directory.GetFiles(Application.persistentDataPath, "*.sav");
        foreach (GameObject box in boxies)
            Destroy(box);
        boxies.Clear();
        foreach (string saveName in saveFileList)
        {
            GameObject newBox = GameObject.Instantiate(dataBoxPrefeb);
            newBox.GetComponent<DataBox>().InitAsLoadBox(Path.GetFileNameWithoutExtension(saveName));
            newBox.transform.SetParent(dataBoxListObj.transform);
            RectTransform rect = (RectTransform)newBox.transform;
            rect.localScale = Vector2.one;
            boxies.Add(newBox);
        }
        addNewDataButton.onClick.RemoveAllListeners();
        addNewDataButton.onClick.AddListener(() =>
        {
            DataManager.StartAsNew();
            PlayUIManager.instance.FadeOutForNextScene("Intro");
            DataSelector.instance.SetSaveMode();
        });
        addNewDataButton.transform.SetAsLastSibling();
    }

    public void SetSaveMode()
    {
        saveFileList = Directory.GetFiles(Application.persistentDataPath, "*.sav");
        foreach (GameObject box in boxies)
            Destroy(box);
        boxies.Clear();
        foreach (string saveName in saveFileList)
        {
            GameObject newBox = GameObject.Instantiate(dataBoxPrefeb);
            newBox.GetComponent<DataBox>().InitAsSaveBox(Path.GetFileNameWithoutExtension(saveName));
            newBox.transform.SetParent(dataBoxListObj.transform);
            RectTransform rect = (RectTransform)newBox.transform;
            rect.localScale = Vector2.one;
            boxies.Add(newBox);
        }
        addNewDataButton.gameObject.SetActive(true);
        addNewDataButton.onClick.RemoveAllListeners();
        addNewDataButton.onClick.AddListener(() =>
        {
            DataManager.currentData.fileIndex = Directory.GetFiles(Application.persistentDataPath, "*.sav").Length;
            string saveName = DataManager.currentData.dataName;
            string originName = DataManager.currentData.dataName_before;
            DataManager.currentData.dataName_before = saveName;
            
            DataManager.Save(saveName);
            //덮어쓰기를 한 후에는
            //어떤 데이터로 계속 갱신할 것인가.
            //기존의 데이터로 계속 하려면 -> 새로 만든 데이터 저장 후 기존 데이터 불러오기.
            DataManager.Load(originName);
            //새로 만든 데이터로 계속하려면
            //DataManager.currentData.dataName_before = saveName;
            SetSaveMode();


        });
        addNewDataButton.transform.SetAsLastSibling();
    }
    public void SetDeleteMode()
    {
        saveFileList = Directory.GetFiles(Application.persistentDataPath, "*.sav");
        foreach (GameObject box in boxies)
            Destroy(box);
        boxies.Clear();
        foreach (string saveName in saveFileList)
        {
            GameObject newBox = GameObject.Instantiate(dataBoxPrefeb);
            newBox.GetComponent<DataBox>().InitAsDeleteBox(Path.GetFileNameWithoutExtension(saveName));
            newBox.transform.SetParent(dataBoxListObj.transform);
            RectTransform rect = (RectTransform)newBox.transform;
            rect.localScale = Vector2.one;
            boxies.Add(newBox);
        }
        addNewDataButton.gameObject.SetActive(false);
    }
}
