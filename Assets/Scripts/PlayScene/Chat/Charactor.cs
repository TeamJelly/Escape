﻿using System.Collections;
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
        var temp = nowEmotion;

        foreach (GameObject emotion in Emotions)
            if (emotion.name == name)
                nowEmotion = emotion;

        if (nowEmotion == temp)
            return;

        PlayUIManager.instance.FadeOut(GetComponent<CanvasGroup>(), () => {
            foreach (GameObject emotion in Emotions)
                emotion.SetActive(false);
            nowEmotion.SetActive(true);
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
