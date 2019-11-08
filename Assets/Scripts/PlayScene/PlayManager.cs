using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public static PlayManager instance;

    public PlayerData data;

    public Item usingItem;


    private void Awake()
    {
        instance = this;
        data = DataManager.currentData;
    }

    public void AddHeart(int i) { data.Heart += i; }
    public void SubHeart(int i) { data.Heart -= i; }
    public void AddTime(int i) { data.Time += i; }
    public void SubTime(int i) { data.Time -= i; }

    public void GetItem(Item item) { }// data.items.Add(item); }
}
