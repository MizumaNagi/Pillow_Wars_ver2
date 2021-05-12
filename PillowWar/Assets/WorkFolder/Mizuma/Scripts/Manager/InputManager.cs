using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingletonMonoBehaviour<InputManager>
{
    [SerializeField] private bool isUseKeyboard = false;

    [SerializeField] private InputData[] playerInput;

    private CharacterMover characterMover = new CharacterMover();

    public MoveData moveData;

    private void Update()
    {
        PlayerManager p = PlayerManager.Instance;
        CharacterData c;

        for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            c = p.charaDatas[i];
            Vector3 moveInput = playerInput[i].MoveAxis;
            Vector3 viewMoveInput = playerInput[i].ViewPointMoveAxis;

            if (moveInput.magnitude > 0.2f) characterMover.Move(moveInput, c);
            if (viewMoveInput.magnitude > 0.2f) characterMover.ViewMove(viewMoveInput, c);

            if (Input.GetButtonDown(playerInput[i].Jump)) characterMover.Jump(c);
            if (Input.GetAxis(playerInput[i].SwitchToADS) < -0.2f) characterMover.ToADS(c);
            else characterMover.ToNonADS(c);
            if (Input.GetAxis(playerInput[i].PillowThrow) > 0.2f && PlayerManager.Instance.charaDatas[i].remainthrowCT < 0) characterMover.PillowThrow(c);
        }

        // ※テスト用※ キーボード移動操作 1Pのみ移動
        if (isUseKeyboard == false) return;
        c = PlayerManager.Instance.charaDatas[0];

        KeyboardInputViewMove(c);
        if (Input.anyKey == false) return;

        KeyboardInputMove(c);
        if (Input.GetKeyDown(KeyCode.Space)) characterMover.Jump(c);
        if (Input.GetMouseButton(1)) characterMover.ToADS(c);
        else characterMover.ToNonADS(c);
    }

    private void KeyboardInputMove(CharacterData c)
    {
        if (Input.GetKey(KeyCode.W)) characterMover.Move(Vector3.forward, c);
        if (Input.GetKey(KeyCode.A)) characterMover.Move(Vector3.left, c);
        if (Input.GetKey(KeyCode.S)) characterMover.Move(Vector3.back, c);
        if (Input.GetKey(KeyCode.D)) characterMover.Move(Vector3.right, c);
    }

    private void KeyboardInputViewMove(CharacterData c)
    {
        Vector3 vec = new Vector3(Input.GetAxis("Mouse X"), 0f, Input.GetAxis("Mouse Y"));
        characterMover.ViewMove(vec, c);
    }
}
