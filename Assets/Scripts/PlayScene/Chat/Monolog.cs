using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monolog : MonoBehaviour
{
    [TextArea(3, 5)]
    public string[] messagies;

    public void StartMonolog()
    {
        ChatSystem2.instance.Monologue(messagies);
    }
   
}
