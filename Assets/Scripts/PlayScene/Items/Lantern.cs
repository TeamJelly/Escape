using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Lantern : Slot, IPointerClickHandler
{
    public Image OnLantern;
    public Image OffLantern;
    public Text LanternText;
    public GameObject lights;

    public new void Start()
    {
        lights = GameObject.FindGameObjectWithTag("light");

        if (DataManager.GetStates()["손전등킴"] == true)
            TurnOnLantern();
        else
            TurnOffLantern();

        base.Start();
    }

    public new void OnPointerClick(PointerEventData eventData)
    {
        if (DataManager.GetStates()["손전등킴"] == true)
            TurnOffLantern();
        else
            TurnOnLantern();
    }

    public void TurnOnLantern()
    {
        lights.SetActive(true);
        imageTransform.gameObject.GetComponent<Image>().sprite = OnLantern.sprite;
        LanternText.text = "On";
        LanternText.color = new Color(1f, 1f, 0.81f);
        DataManager.SetState("손전등킴", true);
    }

    public void TurnOffLantern()
    {
        lights.SetActive(false);
        imageTransform.gameObject.GetComponent<Image>().sprite = OffLantern.sprite;
        LanternText.text = "Off";
        LanternText.color = new Color(0.207f, 0.207f, 0.207f);
        DataManager.SetState("손전등킴", false);
    }

}
