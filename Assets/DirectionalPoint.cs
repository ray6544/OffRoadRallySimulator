using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.DeleteDirectionalPoint(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
