using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
public class ScreenManager : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer Map;
    public Light2D light2D;
    private void Awake()
    {
        float spriteSizeRatio = Map.sprite.rect.size.y / Map.sprite.rect.size.x;
        float needSizeX = Screen.height / spriteSizeRatio;
        if (needSizeX > Screen.width)
        {
            float ratio = Screen.width / needSizeX;
            Map.gameObject.transform.localScale = Vector3.one * ratio;
            light2D.pointLightInnerRadius *= ratio;
            light2D.pointLightOuterRadius *= ratio;
        }
    }
}
