using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_M3 : Puzzle
{

    private void Awake()
    {
        isMain = true;
        eventID = 21;
    }
    public override void OnEnd()
    {
        isCleared = true;
    }
}
