using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
public class ScreenManager : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer GameScreen;
    public Light2D LanternLight;
    public Light2D OtherLight;
    public Light2D GlobalLight;

    public static ScreenManager instance;

    private void Awake()
    {
        instance = this;

        float spriteSizeRatio = GameScreen.sprite.rect.size.y / GameScreen.sprite.rect.size.x;
        float needSizeX = Screen.height / spriteSizeRatio;
        if (needSizeX > Screen.width)
        {
            float ratio = Screen.width / needSizeX;
            GameScreen.gameObject.transform.localScale = Vector3.one * ratio;

            LanternLight.pointLightInnerRadius *= ratio;
            LanternLight.pointLightOuterRadius *= ratio;

            if (OtherLight != null)
            {
                OtherLight.pointLightInnerRadius *= ratio;
                OtherLight.pointLightOuterRadius *= ratio;
            }
        }
    }

    public void TurnOnLanternLight()
    {
        LanternLight.gameObject.SetActive(true);
        GlobalLight.gameObject.SetActive(true);
    }

    public void TurnOffLanternLight()
    {
        LanternLight.gameObject.SetActive(false);
        if (OtherLight == null || OtherLight.gameObject.activeSelf == false)
            GlobalLight.gameObject.SetActive(false);
    }

    public void TurnOnOtherLight()
    {
        if (OtherLight != null)
            OtherLight.gameObject.SetActive(true);
        GlobalLight.gameObject.SetActive(true);
    }

    public void TurnOffOtherLight()
    {
        if (OtherLight != null)
            OtherLight.gameObject.SetActive(false);
        if (LanternLight.gameObject.activeSelf == false)
            GlobalLight.gameObject.SetActive(false);
    }
}