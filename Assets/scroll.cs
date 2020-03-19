﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scroll : MonoBehaviour
{
    public float scrollSpeed;

    private Renderer renderer;
    private Vector2 savedOffset;

    void Start()
    {
        renderer = GetComponent<Renderer>();

        var height = 2 * Camera.main.orthographicSize; 
        var width = height * Camera.main.aspect;

        ((SpriteRenderer)renderer).size = new Vector2(width, height);
        //((RectTransform)transform).sizeDelta = 
    }

    void Update()
    {
        float x = Mathf.Repeat(Time.time * scrollSpeed, 1);
        Vector2 offset = new Vector2(x, x);
        renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
        
    }
}
