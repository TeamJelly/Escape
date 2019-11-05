using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl2 : MonoBehaviour
{
    public float sensitivityY = 4F;
    public float sensitivityX = 4F;
    public float followSpeed = 10f;

    private void Start()
    {
        StartCoroutine(UpdateCamAngle());
    }
    private IEnumerator UpdateCamAngle()
    {

        while (true)
        {
            Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldObjPos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.LookAt(worldObjPos);
            yield return new WaitForEndOfFrame();
        }
    }
}
