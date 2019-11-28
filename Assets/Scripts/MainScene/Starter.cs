using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    public GameObject LoadButton;
    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Log(Application.persistentDataPath);
        DataManager.Load();
        if (DataManager.currentData == null)
            LoadButton.SetActive(false);
       
    }
}
