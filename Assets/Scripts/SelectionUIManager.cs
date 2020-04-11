using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionUIManager : MonoBehaviour
{
    public static SelectionUIManager instance;
    public GameObject SelectionPanel;
    public GameObject ButtonPrefab;
    //버튼을 만든다.
    //버튼에 기능을 추가한다.

    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }

/*    private void Start()
    {
        MakeButton("테스트용 버튼");
        MakeButton("테스트용 버튼");
        MakeButton("테스트용 버튼");
        MakeButton("테스트용 버튼");
    }*/

    public void MakeButton(string text, string goal)
    {
        GameObject tempButton = Instantiate(ButtonPrefab, Vector3.one, Quaternion.identity, SelectionPanel.transform);
        tempButton.GetComponent<Button>().onClick.AddListener(() => {
            ChatSystem2.instance.Go(goal); 
            DeleteAllButton(); 
            SelectionPanel.SetActive(false); 
        });
        tempButton.GetComponentInChildren<Text>().text = text;
    }

    public void DeleteAllButton()
    {
        Transform[] childList = SelectionPanel.GetComponentsInChildren<Transform>(true);
        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                    Destroy(childList[i].gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
