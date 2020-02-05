using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBaloonManager : MonoBehaviour
{

    public Button[] baloons;
    public Button nextButton;
    public Button prevButton;
    int currentIndex = 0;
    // Start is called before the first frame update
    //현재 활성화 되어있는 상호작용 리스트 activatedList
    public List<int> activatedList = new List<int>();
    System.Action[] actions;
    void Start()
    {
        nextButton.gameObject.SetActive(false);
        prevButton.gameObject.SetActive(false);
        //현재 data에서 for문 돌려서 활성화 되어있는 상호작용들을 activatedList에 추가

        actions = new System.Action[activatedList.Count + 2];
        Debug.Log(activatedList.Count);

        actions[0] = () => { Debug.Log("연이와 대화"); };
        actions[1] = () => PlayUIManager.instance.FadeOutForNextScene("PlayScene");
        for (int i = 2; i < actions.Length; i++)
        {
            actions[i] = () => { };
        }
        

        //만약 말풍선개수보다 상호작용개수가 많다면 다음 버튼 활성화
        if(activatedList.Count > baloons.Length)
           nextButton.gameObject.SetActive(true);

        //첫 페이지 리스트 뿌려주기.
        SetBlooms(0);

        //다음버튼 클릭시 동작 지정
        nextButton.onClick.AddListener(() =>
        {
            prevButton.gameObject.SetActive(true);
            currentIndex += baloons.Length;
            SetBlooms(currentIndex);
            //더이상 다음페이지 보여줄게 없으면 버튼 비활성화
            if (currentIndex + baloons.Length >= activatedList.Count)
            {
                nextButton.gameObject.SetActive(false);
            }
        });

        //이전버튼 클릭시 동작 지정
        prevButton.onClick.AddListener(() =>
        {
            nextButton.gameObject.SetActive(true);
            currentIndex -= baloons.Length;
            SetBlooms(currentIndex);
            //더이상 이전페이지 보여줄게 없으면 버튼 비활성화
            if (currentIndex - baloons.Length < 0)
            {
                prevButton.gameObject.SetActive(false);
            }
        });

    }
    void SetBlooms(int index)
    {
        for (int i = 0; i < baloons.Length; i++)
        {
            int thisIndex = i + index;
            if (thisIndex < activatedList.Count)
            {
                baloons[i].gameObject.SetActive(true);
                baloons[i].gameObject.GetComponentInChildren<Text>().text = (thisIndex).ToString();
                //연이 상호작용 db.GetWithID(activatedList[i + index]).title;

                baloons[i].onClick.RemoveAllListeners();
                baloons[i].onClick.AddListener(() =>
                {
                    actions[thisIndex]();
                    //해당 상호작용 클릭시 실행할 동작 지정
                });
            }
            else
            {
                baloons[i].gameObject.SetActive(false);
            }
        }
    }
}
