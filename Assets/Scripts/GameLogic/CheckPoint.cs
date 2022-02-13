using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    bool activated = true;
    public AudioSource _checked;
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.tag);
        if(other.gameObject.tag == "Player")
        {
            Debug.Log(other.tag);
            ActivateCheckPoint();
        }
    }
    public void ActivateCheckPoint(){
        if (activated==true)
        {
            Audio();
            Debug.Log(GameManager.instance.CheckPoints);
            GameManager.instance.RespawnSavePos(this.gameObject.transform);
            activated = false;
            GameManager.instance.CheckPoints++;
            Debug.Log(GameManager.instance.CheckPoints);
            UiManager.instance.CheckPointsUI(GameManager.instance.CheckPoints.ToString() + "/" + GameManager.instance.MaxCheckPoints.ToString());
            UiManager.instance.CheckPointsAnimation();
            GameManager.instance._time += 10f;
            UiManager.instance.TimerTxtAnimation();
        }
    }
    public void Audio()
    {
        if (SoundManager.instance.GetSoundFx() == 0)
        {
            _checked.Play();
        }
    }
}
