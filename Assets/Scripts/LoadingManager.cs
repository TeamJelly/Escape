using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static string nextScene;
    public Slider slider;
    public static void LoadScene(string sceneName) // 다른씬에서 로딩씬으로 넘어갈 때 이용.
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
    void Start()
    {
        StartCoroutine(StartLoad());
    }
    public IEnumerator StartLoad()
    {
        AsyncOperation  async_operation = SceneManager.LoadSceneAsync(nextScene);
        async_operation.allowSceneActivation = false;
        //while (async_operation.progress < 0.9f)
        //{
        //    slider.value = async_operation.progress;
        //    yield return new WaitForEndOfFrame();
        //}
        float ratio = 0;
        while (ratio < 1)
        {
            ratio += 0.02f;
            slider.value = ratio;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        async_operation.allowSceneActivation = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
