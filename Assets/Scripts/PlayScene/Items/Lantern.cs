using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lantern : MonoBehaviour
{
    public GameObject lanternLight = null;
    public Button button;
    public GameObject onLight;
    public GameObject offLight;
    private void Awake()
    {
        button.onClick.AddListener(() =>
        {
            if(lanternLight != null)
                lanternLight.SetActive(!lanternLight.activeSelf);
            onLight.SetActive(!onLight.activeSelf);
            offLight.SetActive(!offLight.activeSelf);
        });
    }
}
