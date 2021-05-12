using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float limitRotY = 14f;

    void Update()
    {
        var localAngle = transform.localEulerAngles;

        if (localAngle.x > limitRotY && localAngle.x < 180) localAngle.x = limitRotY;
        if (localAngle.x < 360 - limitRotY && localAngle.x > 180) localAngle.x = 360 - limitRotY;

        transform.localEulerAngles = localAngle;
    }
}
