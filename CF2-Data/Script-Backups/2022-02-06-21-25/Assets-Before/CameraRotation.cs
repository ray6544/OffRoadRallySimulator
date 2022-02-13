using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    float Roty;
    public float RotationSpeed;
    float RotYClamped;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Roty += Input.GetAxis("Mouse X") *RotationSpeed* Time.deltaTime;
        RotYClamped = Mathf.Clamp(Roty, -175f, -15f);
        transform.eulerAngles = new Vector3(0, RotYClamped, 0);
    }
}
