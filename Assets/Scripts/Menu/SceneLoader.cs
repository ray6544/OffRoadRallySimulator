using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    public int nextScene = 1;
    public Slider progressSlider;
    public void LoadMenu(){
        StartCoroutine(spoofLoading(1));
        
    }
    IEnumerator LoadSceneAsyncronously(int sceneIndex){
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress/.9f);
            progressSlider.value = progress;
            yield return null;
        }
    }
    IEnumerator spoofLoading(int sceneIndex){
        int counter = 1;
        while(counter <= 100){
            counter = counter + Random.Range(1,10);
            float progress = Mathf.Clamp01(counter/100.0f);
            progressSlider.value = progress;
            yield return new WaitForSeconds(Random.Range(0.1f,0.5f));
        }
    }
}
