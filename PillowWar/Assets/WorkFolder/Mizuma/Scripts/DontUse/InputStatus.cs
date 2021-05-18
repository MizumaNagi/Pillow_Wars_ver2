using UnityEngine;
using UnityEngine.InputSystem;

public enum ButtonStatus
{
    Down,
    Up,
    Hold
}

public class InputStatus
{
    public Vector2 leftStickVec;
    public Vector2 rightStickVec;

    // ÉvÉåÉCÉÑÅ[ëÄçÏ
    public void OnMove(InputValue value)
    {
        leftStickVec = new Vector3(value.Get<Vector2>().x, 0f, value.Get<Vector2>().y);
    }

    public void OnViewMove(InputValue value)
    {
        rightStickVec = new Vector3(value.Get<Vector2>().x, 0f, value.Get<Vector2>().y);
    }

    public void OnToADS(InputValue value)
    {
        bool isPress = value.isPressed;

        Debug.Log(isPress);
        Debug.Log("OnToADS");
    }

    public void OnGoOption(InputValue value)
    {
    }

    public void OnInteract(InputValue value)
    {
    }

    public void OnJump(InputValue value)
    {
    }

    public void OnPillowThrow(InputValue value)
    {
    }

    public void OnSquat(InputValue value)
    {
    }

    public void OnRun(InputValue value)
    {
    }

    // UIëÄçÏ
    public void OnCursolMove(InputValue value)
    {
        Vector2 inputVec = value.Get<Vector2>();

        if (inputVec.x > inputVec.y)
        {
            if (inputVec.x > 0)
            {
                PushDpadUp();
            }
            else
            {
                PushDpadDown();
            }
        }
        else
        {
            if (inputVec.y > 0)
            {
                PushDpadLeft();
            }
            else
            {
                PushDpadRight();
            }
        }
    }

    public void PushDpadUp()
    {

    }

    public void PushDpadDown()
    {

    }

    public void PushDpadLeft()
    {

    }

    public void PushDpadRight()
    {

    }

    public void OnOK(InputValue value)
    {

    }

    public void OnCancel(InputValue value)
    {

    }
}