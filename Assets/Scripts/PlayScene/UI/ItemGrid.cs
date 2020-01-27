using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGrid : MonoBehaviour
{
    class Slot
    {
        public Item item;
        public GameObject obj;
        public Slot(Item item, GameObject obj)
        {
            this.item = item;
            this.obj = obj;
        }  
    }
    public RectTransform gridPanel;
    public GameObject slot;

    Slot selected;
    public void addItem(Item item)
    {
        RectTransform obj = Instantiate(slot).GetComponent<RectTransform>();
        obj.GetComponentInChildren<Text>().text = item.itemName;
        obj.SetParent(gridPanel);
        obj.sizeDelta = Vector2.one;
        obj.SetAsLastSibling();

        Slot newSlot = new Slot(item, obj.gameObject);

        obj.GetComponent<Button>().onClick.AddListener(() => selected = newSlot);
    }
    public void subItem()
    {
        Destroy(selected.obj);
    }
}
