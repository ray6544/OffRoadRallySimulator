using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed = 25f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,Speed*Time.deltaTime,0);
    }
}
