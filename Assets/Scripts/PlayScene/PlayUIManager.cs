using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayUIManager : MonoBehaviour
{
    // Start is called before the first frame update

    public PlayerData playerdata;
    public Text FileName;
    public Text Heart;
    public Text Heart2;
    public Text Time;
    public Text Event;
    void Start()
    {
        playerdata = DataManager.currentData;
        FileName.text = "File Name : " + playerdata.name;
        Heart.text = playerdata.Heart.ToString();
        Heart2.text = "X " + playerdata.Heart.ToString();
        Time.text = playerdata.Time.ToString();
        Event.text = playerdata.EventPrograss.ToString();
    }
    public void AddHeart(int v)
    {
        playerdata.Heart += v;
        Heart.text = playerdata.Heart.ToString();
        Heart2.text = "X " + playerdata.Heart.ToString();
    }
    public void AddTime(int v)
    {
        playerdata.Time += v;
        Time.text = playerdata.Time.ToString();
    }

    public void AddEvent(int v)
    {
        playerdata.EventPrograss += v;
        Event.text = playerdata.EventPrograss.ToString();
    }
    // Update is called once per frame
   
}
