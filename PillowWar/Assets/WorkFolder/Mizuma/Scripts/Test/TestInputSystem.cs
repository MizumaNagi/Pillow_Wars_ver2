using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class TestInputSystem : MonoBehaviour
{
    private Gamepad gamePad = Gamepad.current;

    public bool isPressButtonCheck;
    public bool isAxisCheck;
    public bool isDualShockCheck;

#if UNITY_EDITOR
    private void Update()
    {
        gamePad = Gamepad.current;
        if (gamePad == null)
        {
            Debug.LogWarning("コントローラー未接続");
            return;
        }

        if (isPressButtonCheck) CheckIsPressButton();
        if (isDualShockCheck) CheckDualShock();
        if (isAxisCheck) CheckAxisValue();
        
    }

    public void CheckIsPressButton()
    {
        // XBOX用
        // if (gamePad.aButton.isPressed) Debug.Log("a push");
        // if (gamePad.bButton.isPressed) Debug.Log("b push");
        // if (gamePad.xButton.isPressed) Debug.Log("x push");
        // if (gamePad.yButton.isPressed) Debug.Log("y push");

        // PS4用
        if (gamePad.crossButton.isPressed) Debug.Log("cross push");
        if (gamePad.circleButton.isPressed) Debug.Log("circle push");
        if (gamePad.triangleButton.isPressed) Debug.Log("triangle push");
        if (gamePad.squareButton.isPressed) Debug.Log("square push");

        if (gamePad.dpad.up.isPressed) Debug.Log("dpad up push");
        if (gamePad.dpad.down.isPressed) Debug.Log("dpad down push");
        if (gamePad.dpad.left.isPressed) Debug.Log("dpad left push");
        if (gamePad.dpad.right.isPressed) Debug.Log("dpad right push");
        if (gamePad.buttonWest.isPressed) Debug.Log("button west push");
    }

    public void CheckDualShock()
    {
        if (gamePad.circleButton.isPressed)
        {
            InputSystem.ResetHaptics();
        }
        if (gamePad.crossButton.isPressed)
        {
            gamePad.SetMotorSpeeds(0.5f, 0.5f);
        }
        if (gamePad.squareButton.isPressed)
        {
            InputSystem.PauseHaptics();
        }
        if (gamePad.triangleButton.isPressed)
        {
            InputSystem.ResumeHaptics();
        }
    }

    public void CheckAxisValue()
    {
        Debug.Log("left stick axis: " + gamePad.leftStick.ReadValue());
        Debug.Log("right stick axis: " + gamePad.rightStick.ReadValue());
        Debug.Log("right stick sqrMag: " + gamePad.leftStick.ReadValue().sqrMagnitude);
    }

#endif
}
