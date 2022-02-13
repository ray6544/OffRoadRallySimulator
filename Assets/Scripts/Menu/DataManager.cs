using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance { get; private set; }
    [SerializeField]
    public int CarId;
    [SerializeField]
    public int LevelNumber;
    [SerializeField]
    public int Coins;
    public int NumberOfVehicle;
    [Header("Vehicle Properties")]
    public int[] Accel;
    public int[] speed;
    public int[] Braking; 
    public int[] Handling;
    private void Awake()
    {
        Init();
        VariablesInit();
    }
    void Init()
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
    void VariablesInit()
    {
        CarId = PlayerPrefs.GetInt("CarId");
        LevelNumber = PlayerPrefs.GetInt("LevelNumber");
        Coins = PlayerPrefs.GetInt("Coins", 0);
        for (int i = 0; i < NumberOfVehicle; i++)
        {
            Accel[i] = PlayerPrefs.GetInt("Accel"+i);
            speed[i] = PlayerPrefs.GetInt("Speed" + i);
            Braking[i] = PlayerPrefs.GetInt("Braking" + i);
            Handling[i] = PlayerPrefs.GetInt("Handling" + i);
        }
    }
    //*******************************Setters**************************************

    public void SetCarId(int i)
    {
        CarId = i;
        PlayerPrefs.SetInt("CarId",CarId);
    }
    public void SetLevelNumber(int i)
    {
        LevelNumber = i;
        PlayerPrefs.SetInt("LevelNumber", LevelNumber);
    }
    public void SetCoins(int i)
    {
        Coins = i;
        PlayerPrefs.SetInt("Coins", Coins);
    }
    public void SetAccel(int i)
    {
        Accel[CarId] = i;
        PlayerPrefs.SetInt("Accel"+LevelNumber, Accel[CarId]);
    }
    public void SetSpeed(int i)
    {
        speed[CarId] = i;
        PlayerPrefs.SetInt("Speed", speed[CarId]);
    }
    public void SetBraking(int i)
    {
        Braking[CarId] = i;
        PlayerPrefs.SetInt("Braking", Braking[CarId]);
    }
    public void SetHandling(int i)
    {
        Handling[CarId] = i;
        PlayerPrefs.SetInt("Handling", Handling[CarId]);
    }
    //*******************************Getters**************************************
    public int GetCarId()
    {
        return CarId;
    }
    public int GetLevelNumber()
    {
        return LevelNumber;
    }
    public int GetCoins()
    {
        return Coins;
    }
    public int GetAccel()
    {
        return Accel[CarId];
    }
    public int GetSpeed()
    {
        return speed[CarId];
    }
    public int GetBraking()
    {
        return Braking[CarId];
    }
    public int GetHandling()
    {
        return Handling[CarId];
    }
}
