using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public Canvas SplashScreen;
    public Canvas MenuScreen;
    public Canvas GarageScreen;
    public Canvas TruckScreen;
    public Canvas LevelScreen;
    public Slider progressSlider;
    public AudioSource audioSource;
    public Sprite volumeOn;
    public Sprite volumeOff;
    public void LoadSplash()
    {
        StartCoroutine(spoofLoading(0));
    }
    public void LoadMenu()
    {
        StartCoroutine(spoofLoading(1));
    }
    public void LoadGarageView()
    {
        StartCoroutine(spoofLoading(2));
    }
    public void LoadTruckView()
    {
        StartCoroutine(spoofLoading(3));
    }
    public void LoadLevelScreen()
    {
        StartCoroutine(spoofLoading(4));
    }
    public void LoadGame()
    {
        StartCoroutine(LoadSceneAsyncronously(1));
    }
    public void ToggleSound(Image img)
    {
        if(audioSource!=null){
            audioSource.enabled = !audioSource.enabled;
            img.sprite = audioSource.enabled? this.volumeOn : this.volumeOff;
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void PrivacyPolicy(){
        Application.OpenURL ("https://dzynerz.github.io/");
    }
    public void PlayStore(){
        Application.OpenURL ("market://dev?id=6629085804522848973");
    }
    public void PlayStoreRating(){
        Application.OpenURL ("market://details?id=com.DzynerZ.monster.truck.legend");
    }
    IEnumerator LoadSceneAsyncronously(int sceneIndex)
    {
        progressSlider.value = 0;
        MenuScreen.enabled = false;
        GarageScreen.enabled = false;
        TruckScreen.enabled = false;
        LevelScreen.enabled = false;
        SplashScreen.enabled = true;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            progressSlider.value = progress;
            yield return null;
        }
    }
    IEnumerator spoofLoading(int sceneIndex)
    {
        SplashScreen.enabled = false;
        MenuScreen.enabled = false;
        GarageScreen.enabled = false;
        TruckScreen.enabled = false;
        LevelScreen.enabled = false;
        switch (sceneIndex)
        {
            case 0:
                // Show Splash
                SplashScreen.enabled = true;
                int counter = 1;
                while (counter <= 100)
                {
                    counter = counter + Random.Range(1, 10);
                    float progress = Mathf.Clamp01(counter / 100.0f);
                    progressSlider.value = progress;
                    yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
                }
                SplashScreen.enabled = false;
                MenuScreen.enabled = true;
                break;
            case 1:
                MenuScreen.enabled = true;
                // Menu
                break;
            case 2:
                // Garage View
                GarageScreen.enabled = true;
                break;
            case 3:
                // Truck View
                TruckScreen.enabled = true;
                break;
            case 4:
                // Level Screen
                LevelScreen.enabled = true;
                break;
        }
    }
}
