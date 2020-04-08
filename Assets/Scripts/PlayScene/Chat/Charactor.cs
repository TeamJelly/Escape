using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charactor : MonoBehaviour
{
    public string charactorName;
    public Image image;

    public void SetEmotion(string emotion)
    {
        Sprite sprite = Resources.Load("Charactors" + "/" + charactorName + "/" + emotion,typeof(Sprite)) as Sprite;
        //Debug.Log("Charactors" + "/" + charactorName + "/" + emotion);
        if (emotion != "-" && sprite != null)
            image.sprite = sprite;
    }

}
