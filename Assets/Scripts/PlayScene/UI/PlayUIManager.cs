using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerData playerdata;
    public Text Heart;
    public Text Heart2;
    public Text Time;


    void Start()
    {
        playerdata = DataManager.currentData;
        Heart.text = playerdata.Heart.ToString();
        Heart2.text = "X " + playerdata.Heart.ToString();
        Time.text = playerdata.Time.ToString();
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
    // Update is called once per frame
   

}
