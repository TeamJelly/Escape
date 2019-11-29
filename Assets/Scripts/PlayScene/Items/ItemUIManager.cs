using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIManager : MonoBehaviour
{
    public GameObject SelectActionMenu;
    public Text title;
    public Button GetButton;
    public Button InteractButton;
    public Button ObserveButton;

    public GameObject GetUI;
    public Text itemName;
    public Text itemDescription;

    public GameObject InteractUI;
    public GameObject ObeseveUI;
    
    ItemObject currentItem;
    
    public void EnableUI(ItemObject itemObj)
    {
        currentItem = itemObj;
        title.text = itemObj.item.itemName;
        SelectActionMenu.SetActive(true);

        GetButton.onClick.RemoveAllListeners();
        GetButton.onClick.AddListener(() => SelectActionMenu.SetActive(false));
        GetButton.onClick.AddListener(ClickGet);
       
        InteractButton.onClick.RemoveAllListeners();
        InteractButton.onClick.AddListener(ClickInteract);

        ObserveButton.onClick.RemoveAllListeners();
        ObserveButton.onClick.AddListener(ClickObserve);
    }

    public void ClickObserve()
    {
        ObeseveUI.SetActive(true);
        //뭔가 부연설명? 해줄듯.
    }

    public void ClickGet()
    {
        itemName.text = currentItem.item.itemName;
        itemDescription.text = currentItem.item.itemDescription;
        GetUI.SetActive(true);
        DataManager.currentData.items[currentItem.item.ID] = 1;
        DataManager.Save();
        currentItem.DisableItem();
    }

    public void ClickInteract()
    {
        InteractUI.SetActive(true);
        GetComponent<InteractItemUI>().Enable();
        //여기서 아이템 인벤토리 창처럼 띄워서 보여주기
        //선택하면 해당 아이템과 상호작용.
    }
}
