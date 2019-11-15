using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PanoramaTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    
    public void Caputre()
    {
        File.WriteAllBytes("C:/Users/xnaud/2dTest/Assets/Captured.PNG", I360Render.Capture(8192));
    }
}
