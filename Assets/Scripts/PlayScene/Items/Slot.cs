using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public GameObject PopUp;
    public Transform imageTransform;

    public void Start()
    {
        Sprite sprite = imageTransform.gameObject.GetComponent<Image>().sprite;
        if (sprite != null)
        {
            RectTransform newSize = (RectTransform)imageTransform;
            Vector2 spriteSize = sprite.rect.size;
            if (spriteSize.x > spriteSize.y)
                newSize.sizeDelta = new Vector2(180, spriteSize.y / spriteSize.x * 180);
            else
                newSize.sizeDelta = new Vector2(spriteSize.x / spriteSize.y * 180, 180);
        }
    }

}
