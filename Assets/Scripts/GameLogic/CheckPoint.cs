using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool activated = false;
    public CheckpointManager manager;
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.tag);
        if(other.tag.Equals("Player")){
            ActivateCheckPoint();
        }
    }
    public void ActivateCheckPoint(){
        activated = true;
        manager.UpdateCheckPoint(this);
    }
}
