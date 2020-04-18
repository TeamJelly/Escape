using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public CanvasGroup fadePanel;
    //    public GameObject currentPanel;
    public GameObject PopUpPanel;
    public GameObject PopUpContent;
    public Button PopUpBackButton;

    public CanvasGroup noticeGetItemUI;
    public Image getItemImage;

    public CanvasGroup WarningUI;
    public Button WarningYesButton;
    public Button WarningNoButton;
    public Text WarningText;

    public static PlayUIManager instance;

    private void Awake()
    {
        instance = this;
        Debug.Log("PlayUIManager Awake");
        FadeIn(() => { });
        WarningNoButton.onClick.AddListener(() => { FadeOut(WarningUI); });
    }

    public void SetPopUp(GameObject PopUpObject)
    {
        Transform parentTransform =  PopUpObject.transform.parent;
        PopUpObject.transform.SetParent(PopUpContent.transform, false);

        Sprite sprite = PopUpObject.GetComponent<Image>().sprite;
        RectTransform newSize = (RectTransform)PopUpObject.transform;
        Vector2 spriteSize = sprite.rect.size;
        if (spriteSize.x > spriteSize.y)
            newSize.sizeDelta = new Vector2(1320, spriteSize.y / spriteSize.x * 1320);
        else
            newSize.sizeDelta = new Vector2(spriteSize.x / spriteSize.y * 720, 720);

        PopUpObject.SetActive(true);
        FadeIn(PopUpPanel.GetComponent<CanvasGroup>());

        PopUpBackButton.onClick.AddListener(() => {
            FadeOut(PopUpPanel.GetComponent<CanvasGroup>(), () => {
                PopUpObject.transform.SetParent(parentTransform, false);
                PopUpObject.SetActive(false);
                PopUpBackButton.onClick.RemoveAllListeners();
            });
        });
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
        WarningText.text = text;
        WarningYesButton.onClick.RemoveAllListeners();
        WarningYesButton.onClick.AddListener(() => { action(); FadeOut(WarningUI); });
        FadeIn(WarningUI);
    }
    //인벤토리바에 아이템들 띄우기
    /*public void Move(GameObject area)
    {
        fadePanel.interactable = false;
         FadeOut(() =>
         {
             currentPanel.SetActive(false);
             currentPanel = area;
             currentPanel.SetActive(true);
             FadeIn(() => { fadePanel.interactable = true;});
         });
    }*/

    public void FadeIn(System.Action onEnd) //어둡게 시작함
    {
        StartCoroutine(DescendAlpha(fadePanel,onEnd));
    }
    public void FadeIn(CanvasGroup fadeObject)
    {
        fadeObject.alpha = 0;
        fadeObject.interactable = false;
        fadeObject.gameObject.SetActive(true);
        StartCoroutine(AscendAlpha(fadeObject,() => { fadeObject.interactable = true; }));
    }
    public void FadeIn(CanvasGroup fadeObject, System.Action onEnd)
    {
        fadeObject.alpha = 0;
        fadeObject.interactable = false;
        fadeObject.gameObject.SetActive(true);
        StartCoroutine(AscendAlpha(fadeObject, () => { fadeObject.interactable = true; onEnd();}));
    }

    public void FadeOut(System.Action onEnd) //어둡게 끝남
    {
        StartCoroutine(AscendAlpha(fadePanel, onEnd));
    }
    public void FadeOut(CanvasGroup fadeObject)
    {
        fadeObject.alpha = 1;
        fadeObject.interactable = false;
        StartCoroutine(DescendAlpha(fadeObject, () => 
        { 
            fadeObject.gameObject.SetActive(false); 
            fadeObject.interactable = true; 
        }));
    }
    public void FadeOut(CanvasGroup fadeObject, System.Action onEnd)
    {
        fadeObject.alpha = 1;
        fadeObject.interactable = false;
        StartCoroutine(DescendAlpha(fadeObject, () =>
        {
            fadeObject.gameObject.SetActive(false);
            fadeObject.interactable = true;
            onEnd();
        }));
    }


    public void FadeOutForNextScene(string sceneName)
    {
        StartCoroutine(AscendAlpha(fadePanel, () => { SceneManager.LoadScene(sceneName);}));
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
