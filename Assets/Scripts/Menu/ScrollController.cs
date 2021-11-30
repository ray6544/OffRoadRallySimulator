using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    public GameObject scrollbar;
    public GameObject[] selectors;
    public GameObject[] levelIcons;
    public float scroll_pos = 0;
    public int currentLevel = 1;
    public float currentPos = 0.0f;
    public float[] positions = { 0.0f, 0.25171f, 0.50126f, 0.74863f, 1.0f };
    Dictionary<int, int> levelPositions;

    void Awake()
    {
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
        levelIcons[currentLevel - 1].transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

    }
    public void selectLevel(GameObject selectedLevel)
    {
        int pos = Array.FindIndex(levelIcons, item => item == selectedLevel);
        foreach (var level in levelIcons)
        {
            level.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        levelIcons[pos].transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        currentLevel = pos + 1;
    }
    public void prevClick()
    {

        if (currentLevel > 1)
        {
            currentLevel--;
            int pos = levelPositions[currentLevel];
            foreach (var level in levelIcons)
            {
                level.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            levelIcons[currentLevel - 1].transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            foreach (var item in selectors)
            {
                item.transform.GetChild(0).gameObject.SetActive(false);
            }
            selectors[pos].transform.GetChild(0).gameObject.SetActive(true);

            currentPos = positions[pos];
            scrollbar.GetComponent<Scrollbar>().value = currentPos;
        }

    }
    public void nextClick()
    {
        if (currentLevel < 10)
        {
            currentLevel++;
            foreach (var level in levelIcons)
            {
                level.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            levelIcons[currentLevel - 1].transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
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
            level.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        levelIcons[currentLevel].transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        currentPos = positions[pos];
        scrollbar.GetComponent<Scrollbar>().value = currentPos;
    }
}
