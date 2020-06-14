using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static TitleManager instance;

    public GameObject white_particle;
    public GameObject dark_particle;

    public Text [] UITexts;
    public Camera _camera;

    Color pink = new Color(1f, 0.6f, 0.6f);
    Color blue = new Color(0, 0.6f, 0.6f);

    bool isWhite = true;

    private void Awake()
    {
        instance = this;
        SetWhiteTheme();
    }

    public void ConvertTitle()
    {
        if (isWhite)
            SetBlackTheme();
        else
            SetWhiteTheme();
    }

    public void SetBlackTheme()
    {
        isWhite = false;
        foreach(Text text in UITexts)
            text.GetComponent<Text>().color = blue;
        _camera.backgroundColor = Color.black;
        dark_particle.SetActive(true);
        white_particle.SetActive(false);
    }

    public void SetWhiteTheme()
    {
        isWhite = true;
        foreach (Text text in UITexts)
            text.GetComponent<Text>().color = pink;
        _camera.backgroundColor = Color.white;
        dark_particle.SetActive(false);
        white_particle.SetActive(true);
    }
}
