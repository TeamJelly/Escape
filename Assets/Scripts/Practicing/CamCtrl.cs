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
    public int MaxXAxis = 60;
    public int MaxYAxis = 60;
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
            if (delta.x > MaxXAxis) delta.x = MaxXAxis;
            else if (delta.x < -MaxXAxis) delta.x = -MaxXAxis;

            delta.y %= 360;
            if (delta.y > MaxYAxis) delta.y = MaxYAxis;
            else if (delta.y < -MaxYAxis) delta.y = -MaxYAxis;

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