using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingRoom : MonoBehaviour
{
    private void Start()
    {
        DataManager.SetState("거실최초입장", true);
    }
}
