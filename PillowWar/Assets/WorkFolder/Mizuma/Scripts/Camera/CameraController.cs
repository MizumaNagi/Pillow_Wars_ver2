using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    void Update()
    {
        Vector3 rot = transform.localEulerAngles;
        float limitRotY = InputManager.Instance.moveData.limitRotY;

        if (rot.x > limitRotY && rot.x < 180) rot.x = limitRotY;
        if (rot.x < 360 - limitRotY && rot.x > 180) rot.x = -limitRotY;

        transform.localEulerAngles = rot;
    }
}
