using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIManager : MonoBehaviour // 맵상에서 아이템 오브젝트 클릭시 활성화 되는 창.
{
    public GameObject SelectActionMenu;
    public Text title;
    public Button GetButton;
    public Button InteractButton;
    public Button ObserveButton;

    public Text itemName;
    public Text itemDescription;

    public GameObject InteractUI;
    public GameObject ObeseveUI;
    
    ItemObject currentItem;
    
    //UI 활성화 하기 전 아이템 오브젝트 받아서 UI 버튼기능 초기화.
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
        itemName.text = currentItem.item.itemName;
        itemDescription.text = currentItem.item.itemDescription;
        ObeseveUI.SetActive(true);
        //뭔가 부연설명? 해줄듯.
    }

    public void ClickGet()
    {
        DataManager.currentData.items[currentItem.item.ID] = 1;
        DataManager.Save();
        currentItem.DisableItem();
        this.gameObject.SetActive(false);
    }

    public void ClickInteract()
    {
        InteractUI.SetActive(true);
        InteractUI.GetComponent<InteractItemUI>().Enable();
        //여기서 아이템 인벤토리 창처럼 띄워서 보여주기
        //선택하면 해당 아이템과 상호작용.
    }
}
