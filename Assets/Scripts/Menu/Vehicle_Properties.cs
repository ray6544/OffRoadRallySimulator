using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle_Properties : MonoBehaviour
{
    public RCC_CarControllerV3 _carConttroller;
    public GameObject RotationCamera;
    public Rigidbody Rigid;
    public void Speed(float Speed)
    {
        _carConttroller.speed += Speed;
    }
    public void Braking(float Braking)
    {
        _carConttroller.brakeTorque += Braking;
    }
    public void Accelration(float accel)
    {
        _carConttroller.maxEngineTorque += accel;
    }
    public void Handling(float Handling)
    {
        _carConttroller.downForce += Handling;
    }
}
