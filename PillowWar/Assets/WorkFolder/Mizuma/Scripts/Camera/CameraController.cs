using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    void Update()
    {
        Vector3 rot = transform.localEulerAngles;

        float limitRotY = InputManager.Instance.moveData.limitRotY;

        // 345 >= 15 ¨ o
        // 15 > 180 ¨ x
        if (rot.x > limitRotY && rot.x < 180) rot.x = limitRotY;
        // 180 > 345 ¨ x
        else if (rot.x > 180 && rot.x < 360 - limitRotY) rot.x = -limitRotY;
        
        transform.localEulerAngles = rot;
    }
}
