using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashLoading : MonoBehaviour
{
    public LoadingManager loadingManager;
    private void Awake() {
        if(loadingManager!=null){
            //loadingManager.LoadSplash();
            loadingManager.LoadLevelScreen();
        }   
    }
}
