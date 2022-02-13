using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    // Start is called before the first frame update
    bool isActivated = true;
    public GameManager _manager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Activation();
        }
    }
    void Activation()
    {
        if (isActivated == true)
        {
            isActivated = false;
            _manager.LevelComplete();
        }
    }
}
