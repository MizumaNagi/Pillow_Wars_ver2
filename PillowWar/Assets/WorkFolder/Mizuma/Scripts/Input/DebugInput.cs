using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// XBOXコントローラーInputサンプル
public class DebugInput : MonoBehaviour
{
    void Update()
    {
        float lStickHorizontalAxis = Input.GetAxis("Xbox_Axis_L_Horizontal_P1");
        float lStickVerticalAxis = Input.GetAxis("Xbox_Axis_L_Vertical_P1");

        float rStickHorizontalAxis = Input.GetAxis("Xbox_Axis_R_Horizontal_P1");
        float rStickVerticalAxis = Input.GetAxis("Xbox_Axis_R_Vertical_P1");

        float dpadHorizontalAxis = Input.GetAxis("Xbox_Axis_DPad_Horizontal_P1");
        float dpadVerticalAxis = Input.GetAxis("Xbox_Axis_DPad_Vertical_P1");

        float lrTriggerValue = Input.GetAxis("Xbox_Trigger_LR_Trigger_P1");

        // 各種ボタン
        if (Input.GetButtonDown("Xbox_Fire_A_P1")) Debug.Log("push A");
        if (Input.GetButtonDown("Xbox_Fire_B_P1")) Debug.Log("push B");
        if (Input.GetButtonDown("Xbox_Fire_X_P1")) Debug.Log("push X");
        if (Input.GetButtonDown("Xbox_Fire_Y_P1")) Debug.Log("push Y");
        if (Input.GetButtonDown("Xbox_Fire_LB_P1")) Debug.Log("push LB");
        if (Input.GetButtonDown("Xbox_Fire_RB_P1")) Debug.Log("push RB");
        if (Input.GetButtonDown("Xbox_Fire_View_P1")) Debug.Log("push View");
        if (Input.GetButtonDown("Xbox_Fire_Menu_P1")) Debug.Log("push Menu");
        if (Input.GetButtonDown("Xbox_Fire_L_Stick_P1")) Debug.Log("push L-Stick");
        if (Input.GetButtonDown("Xbox_Fire_R_Stick_P1")) Debug.Log("push R-Stick");

        // L-Stick-X (-1:左 / 1:右)
        if (lStickHorizontalAxis > 0.3f) Debug.Log("0.3 over axis L-Stick Horizontal");
        if (lStickHorizontalAxis < -0.3f) Debug.Log("-0.3 under axis L-Stick Horizontal");

        // L-Stick-Y (-1:上 / 1:下)
        if (lStickVerticalAxis > 0.3f) Debug.Log("0.3 over axis L-Stick Vertical");
        if (lStickVerticalAxis < -0.3f) Debug.Log("-0.3 under axis L-Stick Vertical");

        // R-Stick-X (-1:左 / 1:右)
        if (rStickHorizontalAxis > 0.3f) Debug.Log("0.3 over axis R-Stick Horizontal");
        if (rStickHorizontalAxis < -0.3f) Debug.Log("-0.3 under axis R-Stick Horizontal");

        // R-Stick-Y (-1:上 / 1:下)
        if (rStickVerticalAxis > 0.3f) Debug.Log("0.3 over axis R-Stick Vertical");
        if (rStickVerticalAxis < -0.3f) Debug.Log("-0.3 under axis R-Stick Vertical");

        // Dpad-X (-1:左 / 1:右)
        if (dpadHorizontalAxis > 0.3f) Debug.Log("0.3f over axis D-Pad Horizontal");
        if (dpadHorizontalAxis < -0.3f) Debug.Log("-0.3f under axis D-Pad Horizontal");

        // Dpad-Y (-1:下 / 1:上)!
        if (dpadVerticalAxis > 0.3f) Debug.Log("0.3 over axis D-Pad Vertical");
        if (dpadVerticalAxis < -0.3f) Debug.Log("-0.3 under axis D-Pad Vertical");

        // LT/RT (-1~0:LT / 0~1:RT)!!!
        if (lrTriggerValue > 0.3f) Debug.Log("0.3 over axis LR-Trigger");
        if (lrTriggerValue < -0.3f) Debug.Log("-0.3 under axis LR-Trigger");
    }
}
