using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public RectTransform Content;
    public GameObject ItemSlotPrefab;

    public List<GameObject> AllSlotList = new List<GameObject>();

    public string currentSelectItem;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        foreach(Slot slot in Content.GetComponentsInChildren<Slot>(true))
            AllSlotList.Add(slot.gameObject);
    }

    public void UpdateInventory()
    {
        foreach (GameObject itemSlot in AllSlotList)
        {
            var userStates = DataManager.GetData().states;
            if (userStates[itemSlot.name + "획득"] == true && userStates[itemSlot.name + "소모"] == false)
                itemSlot.SetActive(true);
            else
                itemSlot.SetActive(false);
        }
    }

    public void GetItem(string itemName)
    {
        PlayUIManager.instance.NoticeGetItem(itemName);

        foreach(GameObject item in AllSlotList)
        {
            if (item.name == itemName)
            {
                DataManager.GetData().states[itemName + "획득"] = true;
                Debug.Log(item.name + "획득!!");
                UpdateInventory();
                DataManager.Save_Auto();
                break;
            } 
            else
                Debug.LogError(itemName + "의 이름을 가진 아이템이 없습니다.");
        }
    }

    public void LoseItem(string itemName)
    {
        foreach (GameObject item in AllSlotList)
        {
            if (item.name == itemName)
            {
                DataManager.GetData().states[itemName + "소모"] = true;
                Debug.Log(item.name + "소모!!");
                UpdateInventory();
                DataManager.Save_Auto();
                break;
            }
        }
    }

    public void disableSlot(string itemName)
    {
        foreach (GameObject item in AllSlotList)
        {
            if (item.name == itemName)
            {
                item.SetActive(false);
                break;
            }
            else
                Debug.LogError(itemName + "의 이름을 가진 아이템이 없습니다.");
        }
    }

    public void EnableInventoryBar()
    {
        gameObject.SetActive(true);
    }
    public void DisableInventoryBar()
    {
        gameObject.SetActive(false);
    }

}