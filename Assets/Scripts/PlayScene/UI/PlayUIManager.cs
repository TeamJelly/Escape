using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        fadeBackground.interactable = false;
         FadeOut(() =>
         {
             currentPanel.SetActive(false);
             currentPanel = area;
             currentPanel.SetActive(true);
             FadeIn(() => { fadeBackground.interactable = true;});
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
        fadeObject.interactable = false;
        fadeObject.gameObject.SetActive(true);
        StartCoroutine(FadeIn(fadeObject,() => { fadeObject.interactable = true; }));
    }
    public void FadeOut(CanvasGroup fadeObject)
    {
        fadeObject.interactable = false;
        StartCoroutine(FadeOut(fadeObject, () => 
        { 
            fadeObject.gameObject.SetActive(false); 
            fadeObject.interactable = true; 
        }));
    }
    public void FadeOutForNextScene(string sceneName)
    {
        StartCoroutine(FadeOut(fadeBackground, () => { SceneManager.LoadScene(sceneName);}));
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
