﻿using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Platinio;
using TMPro;
public class LevelSelectionController : MonoBehaviour
{
    [Header("UI Section")]
    public TextMeshProUGUI Coinstxt;
    [Header("Panels")]
    public Popup ConnectInernetPanel;
    public GameObject Loading_panel;
    public GameObject scrollbar;
    public GameObject[] selectors;
    public GameObject[] levelIcons;
    public float scroll_pos = 0;
    public int currentLevel = 1;
    public float currentPos = 0.0f;
    public float[] positions = { 0.0f, 0.25171f, 0.50126f, 0.74863f, 1.0f };
    Dictionary<int, int> levelPositions;
    [Header("Sound")]
    public AudioSource _audiosource;
    public AudioClip Click, SelectionClick, Alert, congrets;
    void Start()
    {
        Init();
    }
    void Init()
    {
        SoundManager.instance.GameMusic();
        levelPositions = new Dictionary<int, int>();
        levelPositions.Add(1, 0);
        levelPositions.Add(2, 0);
        levelPositions.Add(3, 1);
        levelPositions.Add(4, 1);
        levelPositions.Add(5, 2);
        levelPositions.Add(6, 2);
        levelPositions.Add(7, 3);
        levelPositions.Add(8, 3);
        levelPositions.Add(9, 4);
        levelPositions.Add(10, 4);
        levelIcons[currentLevel].transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        if (PlayerPrefs.GetInt("Levels"+ 0) == 0)
            PlayerPrefs.SetInt("Levels"+0, 1);
        for (int i = 0; i < levelIcons.Length; i++)
        {
            
            if (PlayerPrefs.GetInt("Levels"+i) == 0)
            {
                levelIcons[i].transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (PlayerPrefs.GetInt("Levels"+i) == 1)
            {
                levelIcons[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        DataManager.instance.SetLevelNumber(currentLevel);
        CoinsShow(DataManager.instance.GetCoins());
    }
    
    public void selectLevel(GameObject selectedLevel)
    {
        int pos = Array.FindIndex(levelIcons, item => item == selectedLevel);
        foreach (var level in levelIcons)
        {
            level.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
        levelIcons[pos].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        currentLevel = pos + 1;
    }
    public void prevClick()
    {
        Audio(Click);
        if (currentLevel > 1)
        {
            currentLevel--;
            int pos = levelPositions[currentLevel];
            foreach (var level in levelIcons)
            {
                level.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }
            levelIcons[currentLevel - 1].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            foreach (var item in selectors)
            {
                item.transform.GetChild(0).gameObject.SetActive(false);
            }
            selectors[pos].transform.GetChild(0).gameObject.SetActive(true);

            currentPos = positions[pos];
            scrollbar.GetComponent<Scrollbar>().value = currentPos;
            DataManager.instance.SetLevelNumber(currentLevel);
        }
       
    }
    public void nextClick()
    {
        Audio(Click);
        if (currentLevel < 10)
        {
            currentLevel++;
            foreach (var level in levelIcons)
            {
                level.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }
            levelIcons[currentLevel - 1].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            if (currentLevel <= 9)
            {
                int pos = levelPositions[currentLevel];
                currentPos = positions[pos];
                foreach (var item in selectors)
                {
                    item.transform.GetChild(0).gameObject.SetActive(false);
                }
                selectors[pos].transform.GetChild(0).gameObject.SetActive(true);

                scrollbar.GetComponent<Scrollbar>().value = currentPos;
            }
            DataManager.instance.SetLevelNumber(currentLevel);
        }

    }

    // Formula y1 = 2x, element y2 = 2x+1 e.g. x= 0 ; y1 = 2(0) => 0 ; y2 = 2(0)+1 => 1;
    public void changeSelector(GameObject selector)
    {
        int pos = Array.FindIndex(selectors, item => item == selector);
        foreach (var item in selectors)
        {
            item.transform.GetChild(0).gameObject.SetActive(false);
        }
        selectors[pos].transform.GetChild(0).gameObject.SetActive(true);
        currentLevel = 2 * (pos);
        int nextLevel = 2 * pos + 1;
        foreach (var level in levelIcons)
        {
            level.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
        levelIcons[currentLevel].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        currentPos = positions[pos];
        scrollbar.GetComponent<Scrollbar>().value = currentPos;
    }
    void CoinsShow(int i)
    {
        Coinstxt.text = i.ToString();
    }
    public void AddCoins(int coins)
    {
        Audio(Click);
        if (InternetConnectivity())
        {
            DataManager.instance.SetCoins(DataManager.instance.GetCoins() + coins);
            CoinsShow(DataManager.instance.GetCoins());
        }
        else
        {
            ConnectInernetPanel.Toggle();
        }
    }
    public bool InternetConnectivity()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void NextLevel()
    {
        SoundManager.instance.IsLoading(true);
        Debug.Log(DataManager.instance.GetLevelNumber());
        Loading_panel.GetComponent<Splash>().LoadingSceneName = "Level" + DataManager.instance.GetLevelNumber();
        Loading_panel.SetActive(true);
    }
    public void BackLevel()
    {
        SoundManager.instance.IsLoading(true);
        Loading_panel.GetComponent<Splash>().LoadingSceneName = "MainMenu";
        Loading_panel.SetActive(true);
    }
    public void Audio(AudioClip _audioClip)
    {
        if (SoundManager.instance.GetSoundFx() == 0)
        {
            _audiosource.PlayOneShot(_audioClip);
        }
    }
}
