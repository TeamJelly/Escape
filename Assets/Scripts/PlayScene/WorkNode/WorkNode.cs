using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public delegate void Func();
public abstract class WorkNode : MonoBehaviour
{
    public string buttonText; // 다른노드에서 볼 때 보여지는 텍스트.
    public List<WorkNode> nodes = new List<WorkNode>(); // gui상에서 직접 지정할것.
   [HideInInspector]
    public Button button;
   // public Func OnClick; //자기 자신을 눌렀을 때 실행되는 기능.
    public Transform buttonTransform;
    public Transform cameraTransform;

    [HideInInspector]
    public WorkNode beforeNode = null;
    public abstract void OnClick(WorkNode from);
    public void OnExit()
    {
        HideNodes();
        BackgroundManager.instance.DisableBackButton();
    }
    public void InitNode(Button b)
    {
        foreach (WorkNode node in nodes)
        {
            //node.beforeNode = this;
            node.button = Instantiate(b); // 복제하여 사용.
            node.button.GetComponentInChildren<Text>().text = node.buttonText;
            node.button.transform.SetParent(GameObject.Find("Interactive Buttons").transform);
        }
    }
    public void ShowNodes()
    {
        CamCtrl.instance.SetTransform(transform);
        foreach (WorkNode w in nodes)
            StartCoroutine(ShowButtonOnNode(w));
        
    }

    //버튼 비활성화.
    public void HideNodes()
    {
        foreach (WorkNode w in nodes)
        {
          w.button.gameObject.SetActive(false);
            //Debug.Log(w.name);
        }
    }

    public IEnumerator ShowButtonOnNode(WorkNode n)
    {
        n.button.gameObject.SetActive(true);
        while (n.button.gameObject.activeSelf)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(n.transform.position);
            n.button.transform.position = new Vector3(screenPos.x, screenPos.y, n.button.transform.position.z);
            yield return null;
        }
    }
}
