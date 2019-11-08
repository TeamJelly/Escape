using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CamCtrl : MonoBehaviour
{
    public static CamCtrl instance;
    public Vector3 originAngle;
    public Vector3 delta;
    public Vector2 input;
    public int speed = 6;
    public void Awake()
    {
        instance = this;
        originAngle = Camera.main.transform.eulerAngles;
        delta = Vector3.zero;
    }
    public void LateUpdate()
    {
        if (Input.touchCount > 0)
        {
            input = Input.GetTouch(0).deltaPosition * Time.smoothDeltaTime * speed;
            delta += new Vector3(-input.y, input.x, 0);
            if (delta.x > 60) delta.x = 60;
            else if (delta.x < -60) delta.x = -60;

            if (delta.y > 60) delta.y = 60;
            else if (delta.y < -60) delta.y = -60;

           Camera.main.transform.rotation = Quaternion.Euler(originAngle + delta);
        }
    }
    public void SetTransform(Transform t)
    {
        Camera.main.transform.position = t.position;
        Camera.main.transform.eulerAngles = t.eulerAngles;
        originAngle = t.eulerAngles;
        delta = Vector3.zero;
    }
}