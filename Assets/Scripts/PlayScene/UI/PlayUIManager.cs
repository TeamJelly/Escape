﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public CanvasGroup fadeBackground;
    public GameObject currentPanel;
    public static PlayUIManager instance;

    private void Awake()
    {
        instance = this;
        FadeIn(() => { });
    }
    public void Move(GameObject area)
    {
         FadeOut(() =>
         {
             currentPanel.SetActive(false);
             currentPanel = area;
             currentPanel.SetActive(true);
             FadeIn(() => { });
         });
    }
    public void FadeIn(System.Action onEnd)
    {
        StartCoroutine(FadeIn(fadeBackground,onEnd));
    }
    public void FadeOut(System.Action onEnd)
    {
        StartCoroutine(FadeOut(fadeBackground, onEnd));
    }
    public void FadeIn(CanvasGroup fadeObject)
    {
        fadeObject.alpha = 0;
        fadeObject.gameObject.SetActive(true);
        StartCoroutine(FadeIn(fadeObject,() => { }));
    }
    public void FadeOut(CanvasGroup fadeObject)
    {
        StartCoroutine(FadeOut(fadeObject, () => { fadeObject.gameObject.SetActive(false); }));
    }
    public void FadeOutForNextScene(string sceneName)
    {
        StartCoroutine(FadeOut(fadeBackground, () => { UIFunctions.SelectScene(sceneName);}));
    }
    IEnumerator FadeIn(CanvasGroup fadeObject, System.Action onEnd)
    {
        float tempAlpha = 0;
        float fadeTime = 0.2f;
        while (tempAlpha < 1f)
        {
            fadeObject.alpha = tempAlpha;
            tempAlpha += Time.deltaTime / fadeTime;
            yield return null;
        }
        fadeObject.alpha = 1.0f;
        onEnd();
    }
    IEnumerator FadeOut(CanvasGroup fadeObject, System.Action onEnd)
    {
        float tempAlpha = 1;
        float fadeTime = 0.2f;
        while (tempAlpha > 0)
        {
            fadeObject.alpha = tempAlpha;
            tempAlpha -= Time.deltaTime / fadeTime;
            yield return null;
        }
        fadeObject.alpha = 0;
        onEnd();
    }
}
