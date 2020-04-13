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

    public GameObject TitleText;
    public Camera _camera;
    
    Color blue = new Color(0, 98, 98);

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
        _camera.backgroundColor = Color.black;
        TitleText.GetComponent<Image>().color = blue;
        dark_particle.SetActive(true);
        white_particle.SetActive(false);
    }

    public void SetWhiteTheme()
    {
        isWhite = true;
        _camera.backgroundColor = Color.white;
        TitleText.GetComponent<Image>().color = Color.white;
        dark_particle.SetActive(false);
        white_particle.SetActive(true);
    }
}
