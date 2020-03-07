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

    
    public CanvasGroup noticeGetItemUI;
    public Image getItemImage;

    public CanvasGroup dataWarningUI;
    public Button actionButton;
    public Text warningText;

    public static PlayUIManager instance;

    private void Awake()
    {
        instance = this;
        FadeIn(() => { });
    }

    public void NoticeGetItem(string itemName)
    {
        getItemImage.sprite = Resources.Load("Item/" + itemName, typeof(Sprite)) as Sprite;
        if (getItemImage.sprite != null)
        {
            RectTransform newSize = (RectTransform)getItemImage.transform;
            Vector2 spriteSize = getItemImage.sprite.rect.size;
            if(spriteSize.x > spriteSize.y)
            {
                newSize.sizeDelta = new Vector2(550, spriteSize.y / spriteSize.x * 550);
            }
            else newSize.sizeDelta = new Vector2(spriteSize.x / spriteSize.y * 550, 550);
        }
        FadeIn(noticeGetItemUI);
    }

    public void NoticeDataWarning(string text, System.Action action)
    {
        warningText.text = text;
        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(() => { action(); FadeOut(dataWarningUI); });
        FadeIn(dataWarningUI);
    }
    //인벤토리바에 아이템들 띄우기

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
        StartCoroutine(DescendAlpha(fadeBackground,onEnd));
    }
    public void FadeOut(System.Action onEnd)
    {
        StartCoroutine(AscendAlpha(fadeBackground, onEnd));
    }
    public void FadeIn(CanvasGroup fadeObject)
    {
        fadeObject.alpha = 0;
        fadeObject.interactable = false;
        fadeObject.gameObject.SetActive(true);
        StartCoroutine(AscendAlpha(fadeObject,() => { fadeObject.interactable = true; }));
    }
    public void FadeOut(CanvasGroup fadeObject)
    {
        fadeObject.interactable = false;
        StartCoroutine(DescendAlpha(fadeObject, () => 
        { 
            fadeObject.gameObject.SetActive(false); 
            fadeObject.interactable = true; 
        }));
    }
    public void FadeOutForNextScene(string sceneName)
    {
        StartCoroutine(AscendAlpha(fadeBackground, () => { SceneManager.LoadScene(sceneName);}));
    }

    public IEnumerator AscendAlpha(CanvasGroup fadeObject, System.Action onEnd)
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
    public IEnumerator DescendAlpha(CanvasGroup fadeObject, System.Action onEnd)
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
