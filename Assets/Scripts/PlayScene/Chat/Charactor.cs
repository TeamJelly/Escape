using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charactor : MonoBehaviour
{
    public string [] charactorName;
    public GameObject [] Emotions;

    public GameObject nowEmotion;

    Color listeningColor = new Color(50 / 255f, 50 / 255f, 50 / 255f);
    Color tellingColor = new Color(1, 1, 1);

    private void Awake()
    {
        nowEmotion.SetActive(true);
    }

    public void SetEmotion(string name)
    {
        PlayUIManager.instance.FadeOut(GetComponent<CanvasGroup>(), () => {
            foreach (GameObject emotion in Emotions)
            {
                if (emotion.name == name)
                {
                    emotion.SetActive(true);
                    nowEmotion = emotion;
                }
                else
                {
                    emotion.SetActive(false);
                }
            }
            PlayUIManager.instance.FadeIn(GetComponent<CanvasGroup>());
        });
    }

    public void SetListening()
    {
        nowEmotion.GetComponent<Image>().color = listeningColor;
    }

    public void SetTelling()
    {
        nowEmotion.GetComponent<Image>().color = tellingColor;
    }
}
