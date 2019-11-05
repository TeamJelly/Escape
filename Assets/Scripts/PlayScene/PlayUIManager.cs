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
    public Text Time;
    public Text Event;
    void Start()
    {
        playerdata = DataManager.currentData;
        FileName.text = "File Name : " + playerdata.name;
        Heart.text = playerdata.heart.ToString();
        Time.text = playerdata.time.ToString();
        Event.text = playerdata.evenPrograss.ToString();
    }
    public void AddHeart(int v)
    {
        playerdata.heart += v;
        Heart.text = playerdata.heart.ToString();
    }
    public void AddTime(int v)
    {
        playerdata.time += v;
        Time.text = playerdata.time.ToString();
    }

    public void AddEvent(int v)
    {
        playerdata.evenPrograss += v;
        Event.text = playerdata.evenPrograss.ToString();
    }
    // Update is called once per frame
   
}
