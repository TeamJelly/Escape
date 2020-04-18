using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    public GameObject[] thisSceneItemList;

    private void Awake()
    {
        instance = this;
        Init();
        UpdateSceneItem();
    }

    public void FindAllItem()
    {
        thisSceneItemList = GameObject.FindGameObjectsWithTag("Item");
    }

    public void Init() //씬변경할때 호출해야 한다.
    {
        FindAllItem();
        foreach (GameObject item in thisSceneItemList)
        {
            Button button = item.GetComponent<Button>(); //맵상의 아이템은 Canvas의 버튼일수도 있고
            EventTrigger trigger = item.GetComponent<EventTrigger>(); //오브젝트의 이벤트 트리거일수도 있다.

            if (button != null)
            {
                button.onClick.AddListener(() => {
                    InventoryManager.instance.GetItem(item.name);
                    UpdateSceneItem();
                });
            } else if (trigger != null)
            {
                EventTrigger.Entry entry_PointerClick = new EventTrigger.Entry
                {
                    eventID = EventTriggerType.PointerClick
                };
                entry_PointerClick.callback.AddListener((data) =>
                {
                    InventoryManager.instance.GetItem(item.name);
                    UpdateSceneItem();
                });
                trigger.triggers.Add(entry_PointerClick);
            }
        }
    }

    public void UpdateSceneItem()
    {
        foreach (GameObject item in thisSceneItemList)
        {
            if (DataManager.GetData().states[item.name + "획득"] == true) //획득한적이 있다면 무조건 제거
                item.SetActive(false);
            else
                item.SetActive(true);
        }
    }
}
