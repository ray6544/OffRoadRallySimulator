using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    public int checkPointsCrossed;
    public float[] position;
    public PlayerData(int progress, Vector3 position){
        this.checkPointsCrossed = progress;
        this.position = new float[3];
        this.position[0]= position.x;
        this.position[1]= position.y;
        this.position[2]= position.z;
    }
}
