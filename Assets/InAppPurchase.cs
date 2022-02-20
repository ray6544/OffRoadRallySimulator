using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAppPurchase : MonoBehaviour
{
    public static InAppPurchase instance { set; get; }
    public int NumberOfLevels = 10;
    public int NumberOfVehicles = 5;
    public delegate void UnlockAllLevels();
    public static UnlockAllLevels _unlockAllLevels;
    public delegate void UnlockAllVehicles();
    public static UnlockAllVehicles _unlockAllVehicles;
    private void Awake()
    {
        init();
    }
    void init()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(instance);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UnlockAllLevelsEvent()
    {
        for (int i = 0; i < NumberOfLevels; i++)
        {
            if (PlayerPrefs.GetInt("Levels" + i) == 0)
                PlayerPrefs.SetInt("Levels" + i, 1);
        }
        _unlockAllLevels();
    }
    public void UnlockAllVehiclesEvent()
    {
        for (int i = 0; i < NumberOfLevels; i++)
        {
            if (PlayerPrefs.GetInt("VehicleUnlock" + i) == 0)
                PlayerPrefs.SetInt("VehicleUnlock" + i, 1);
        }
        _unlockAllVehicles();
    }
    public void RemoveAdsEvent()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
            PlayerPrefs.SetInt("RemoveAds", 1);
    }
}
