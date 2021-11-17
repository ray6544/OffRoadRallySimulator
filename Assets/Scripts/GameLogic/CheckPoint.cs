using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool activated = false;
    public CheckpointManager manager;
    private void OnTriggerEnter(Collider other) {
        if(other.tag.Equals("Player")){
            ActivateCheckPoint();
        }
    }
    public void ActivateCheckPoint(){
        activated = true;
        manager.UpdateCheckPoint(this);
    }
}
