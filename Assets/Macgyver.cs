using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Macgyver : Slot, IPointerClickHandler
{
    public Image Spanner;
    public Image Driver;
    public Image Knife;

    public Text MacgyverText;

    public new void Start()
    {
        if (DataManager.GetStates()["맥가이버스패너"] == true)
            SpannerMode();

        else if (DataManager.GetStates()["맥가이버드라이버"] == true)
            DriverMode();

        else if (DataManager.GetStates()["맥가이버나이프"] == true)
            KnifeMode();

        else
            SpannerMode();

        base.Start();
    }

    public new void OnPointerClick(PointerEventData eventData)
    {
        if (DataManager.GetStates()["맥가이버스패너"] == true)
            DriverMode();
        else if (DataManager.GetStates()["맥가이버드라이버"] == true)
            KnifeMode();
        else
            SpannerMode();
    }

    public void SpannerMode()
    {
        imageTransform.gameObject.GetComponent<Image>().sprite = Spanner.sprite;
        MacgyverText.text = "Spanner";

        DataManager.SetState("맥가이버스패너", true);
        DataManager.SetState("맥가이버드라이버", false);
        DataManager.SetState("맥가이버나이프", false);
    }

    public void DriverMode()
    {
        imageTransform.gameObject.GetComponent<Image>().sprite = Driver.sprite;
        MacgyverText.text = "Driver";

        DataManager.SetState("맥가이버스패너", false);
        DataManager.SetState("맥가이버드라이버", true);
        DataManager.SetState("맥가이버나이프", false);
    }

    public void KnifeMode()
    {
        imageTransform.gameObject.GetComponent<Image>().sprite = Knife.sprite;
        MacgyverText.text = "Knife";

        DataManager.SetState("맥가이버스패너", false);
        DataManager.SetState("맥가이버드라이버", false);
        DataManager.SetState("맥가이버나이프", true);
    }

}
