using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private static Vector3 startingPosition;
    public int checkPointsPassed = 0;
    public CheckPoint activeCheckPoint;
    public Text elpasedText;
    public Text checkpointText;
    public static GameObject[] checkpointList;
    private float startTime;
    private float checkpointStart;
    private float checkpointDuration;
    private void Update() {
        float elapsed = Time.time - startTime;
        string minutes = ((int)elapsed/60).ToString();
        string seconds = (elapsed % 60).ToString("f0");
        elpasedText.text = minutes + ":" + seconds;
    }
    private void Awake()
    {
        checkpointList = GameObject.FindGameObjectsWithTag("CheckPoint");
        Debug.Log(checkpointList.Length);
        if (checkpointList != null)
        {
            foreach (GameObject entry in checkpointList)
            {
                entry.GetComponent<CheckPoint>().manager = this;
            }
        }
        PlayerData data = SaveSystem.LoadData();
        if(data!=null){
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            //player.transform.position = new Vector3(data.position[0],data.position[1],data.position[2]);
            checkPointsPassed = data.checkPointsCrossed;
        }
        startTime = Time.time;
        checkpointStart = Time.time;
    }
    public void UpdateCheckPoint(CheckPoint checkpoint)
    {
        checkPointsPassed++;
        foreach (GameObject entry in checkpointList)
        {
            if (entry.GetComponent<CheckPoint>() == checkpoint)
            {
                activeCheckPoint = checkpoint;
            }
            else
            {
                checkpoint.activated = false;
            }
        }
        PlayerData data = new PlayerData(checkPointsPassed,checkpoint.gameObject.transform.position);
        SaveSystem.SaveData(data);
        checkpointDuration =  Time.time - checkpointStart;
        checkpointStart = Time.time;
        string minutes = ((int)checkpointDuration/60).ToString();
        string seconds = (checkpointDuration % 60).ToString("f0");
        checkpointText.text = minutes + ":" + seconds;
    }
    private Vector3 GetActivatedCheckPoint()
    {
        Vector3 result = startingPosition;
        if (checkpointList != null)
        {
            foreach (GameObject checkpoint in checkpointList)
            {
                // We search the activated checkpoint to get its position
                if (checkpoint.GetComponent<CheckPoint>().activated)
                {
                    result = checkpoint.transform.position;
                    break;
                }
            }
        }
        return result;
    }
}
