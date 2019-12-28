using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour //맵상에 배치되는 아이템정보를 담는 게임오브젝트. BackgroundManager에서 초기화.
{
    public int itemID; // 아이템 고유ID
    public List<GameObject> images = new List<GameObject>(); // 맵에서 보이는 아이템 이미지들.
    public Item item;//DB에 저장되어있던 Item 정보. BackgroundManager에서 itemID에 해당하는 정보를 할당받는다.

    public void DisableItem() //맵상에 존재하는 아이템 이미지 비활성화.
    {
        
        foreach (GameObject img in images)
        {
            img.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
