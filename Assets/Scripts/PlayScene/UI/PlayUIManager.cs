using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Image fadeBackground;
    public GameObject currentPanel;
    public void Move(GameObject area)
    {
        StartCoroutine(Fade(area));
    }
    IEnumerator Fade(GameObject area)
    {
        Color tempColor = fadeBackground.color;
        float fadeTime = 0.2f;
        fadeBackground.gameObject.SetActive(true);
        while (tempColor.a < 1f)
        {
            fadeBackground.color = tempColor;
            tempColor.a += Time.deltaTime / fadeTime;
            yield return null;
        }
        tempColor.a = 1.0f;        
        fadeBackground.color = tempColor;

        currentPanel.SetActive(false);
        currentPanel = area;
        currentPanel.SetActive(true);
        
        while (tempColor.a > 0f)
        {
            fadeBackground.color = tempColor;
            tempColor.a -= Time.deltaTime / fadeTime;
            yield return null;
        }
        tempColor.a = 0.0f;
        fadeBackground.color = tempColor;
        fadeBackground.gameObject.SetActive(false);
    }
    // Update is called once per frame
   
}
