using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Splash : MonoBehaviour
{
    bool _load =false;
    AsyncOperation _operation;
    [Header("Scene Name")]
    public string LoadingSceneName;
    public Slider LoadingSlider;
	void Start () 
    {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
        StartCoroutine(LoadingScene(LoadingSceneName));
    }
    IEnumerator LoadingScene(string scene)
    {
        int counter = 1;
        while (counter <= 100)
        {
            counter = counter + Random.Range(1, 10);
            float progress = Mathf.Clamp01(counter / 100.0f);
            LoadingSlider.value = progress;
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
        }
        SceneManager.LoadScene(LoadingSceneName);
        yield return null;
        }
    }
