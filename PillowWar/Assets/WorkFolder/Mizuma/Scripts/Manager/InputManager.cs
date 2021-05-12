using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingletonMonoBehaviour<InputManager>
{
    [SerializeField] private InputData[] playerInput;

    private CharacterMover characterMover = new CharacterMover();

    public MoveData moveData;

    private void Update()
    {
        for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            PlayerManager p = PlayerManager.Instance;
            CharacterData c = p.charaDatas[i];
        
            Vector3 moveInput = playerInput[i].MoveAxis;
            Vector3 viewMoveInput = playerInput[i].ViewPointMoveAxis;

            if (moveInput.magnitude > 0.2f) characterMover.Move(moveInput, c);
            if (viewMoveInput.magnitude > 0.2f) characterMover.ViewMove(viewMoveInput, c);
        }

        // ※テスト用※ キーボード移動操作 1Pのみ移動
        KeyboardInputMove();
        KeyboardInputViewMove();
    }

    private void KeyboardInputMove()
    {
        if (Input.anyKey == false) return;

        CharacterData c = PlayerManager.Instance.charaDatas[0];
        if (Input.GetKey(KeyCode.W)) characterMover.Move(Vector3.forward, c);
        if (Input.GetKey(KeyCode.A)) characterMover.Move(Vector3.left, c);
        if (Input.GetKey(KeyCode.S)) characterMover.Move(Vector3.back, c);
        if (Input.GetKey(KeyCode.D)) characterMover.Move(Vector3.right, c);
    }

    private void KeyboardInputViewMove()
    {
        Vector3 vec = new Vector3(Input.GetAxis("Mouse X"), 0f, Input.GetAxis("Mouse Y"));
        CharacterData c = PlayerManager.Instance.charaDatas[0];
        characterMover.ViewMove(vec, c);
    }
}
