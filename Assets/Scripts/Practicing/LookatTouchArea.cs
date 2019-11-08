using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookatTouchArea : MonoBehaviour
{
    public float sensitivityY = 4F;
    public float sensitivityX = 4F;
    public float followSpeed = 10f;
    public Camera cam;
    private void Start()
    {
        StartCoroutine(UpdateCamAngle());
    }
    private IEnumerator UpdateCamAngle()
    {

        while (true)
        {
            Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector3 worldObjPos = cam.ScreenToWorldPoint(new Vector3(mousePos.x,mousePos.y,-cam.transform.position.z));
            transform.LookAt(worldObjPos);
            yield return new WaitForEndOfFrame();
        }
    }
}
