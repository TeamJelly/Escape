using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemObject : MonoBehaviour //맵상에 배치되는 아이템정보를 담는 게임오브젝트. BackgroundManager에서 초기화.
{
    public int itemID; // 아이템 고유ID
    public List<GameObject> images = new List<GameObject>(); // 맵에서 보이는 아이템 이미지들.

    public void Init()
    {
        foreach (GameObject g in images)
        {
            Button b = g.GetComponent<Button>();
            if (b != null)
                b.onClick.AddListener(() =>
                {
                    BackgroundManager.instance.GetItem(itemID);
                    DisableItem();
                });
        }
    }

    public void DisableItem() //맵상에 존재하는 아이템 이미지 비활성화.
    {
        foreach (GameObject img in images)
        {
            img.SetActive(false);
        }
        //gameObject.SetActive(false);
    }

    public void EnableItem() //맵상에 존재하는 아이템 이미지 활성화.
    {
        foreach (GameObject img in images)
        {
            img.SetActive(true);
        }
        //gameObject.SetActive(true);
    }
}
