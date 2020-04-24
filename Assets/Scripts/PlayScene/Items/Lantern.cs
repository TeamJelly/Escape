using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Lantern : Slot, IPointerClickHandler
{
    public Image OnLantern;
    public Image OffLantern;
    public Text LanternText;

    public static Lantern instance;

    public void Awake()
    {
        instance = this;
    }

    public new void Start()
    {
        if (ScreenManager.instance.OtherLight != null && ScreenManager.instance.OtherLight.gameObject.activeSelf)
            TurnOffLantern();
        else
            TurnOnLantern();
        base.Start();
    }

    public new void OnPointerClick(PointerEventData eventData)
    {
        if (DataManager.GetStates()["손전등킴"] == false)
            TurnOnLantern();
        else
            TurnOffLantern();
    }

    public void TurnOnLantern()
    {
        if (ScreenManager.instance.OtherLight != null && ScreenManager.instance.OtherLight.gameObject.activeSelf)
            ChatSystem2.instance.Monologue("여긴 밝아서 킬 필요가 없을거같다.");
        else
        {
            ScreenManager.instance.TurnOnLanternLight();
            imageTransform.gameObject.GetComponent<Image>().sprite = OnLantern.sprite;
            LanternText.text = "On";
            LanternText.color = new Color(1f, 1f, 0.81f);
            DataManager.SetState("손전등킴", true);
        }
    }

    public void TurnOffLantern()
    {
        ScreenManager.instance.TurnOffLanternLight();
        imageTransform.gameObject.GetComponent<Image>().sprite = OffLantern.sprite;
        LanternText.text = "Off";
        LanternText.color = new Color(0.207f, 0.207f, 0.207f);
        DataManager.SetState("손전등킴", false);
    }

}
