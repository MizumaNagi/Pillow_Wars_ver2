using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class InputManager : SingletonMonoBehaviour<InputManager>
{
    [SerializeField] private bool isUseKeyboard = false;
    [SerializeField] private int keyboardMovePlayerId;

    [SerializeField] private InputData[] playerInput;

    private CharacterMover characterMover = new CharacterMover();
    private List<CharacterData> characterDatas = new List<CharacterData>();
    private GameManager gameManager;

    public MoveData moveData;

    private void Start()
    {
        gameManager = GameManager.Instance;
        characterDatas = PlayerManager.Instance.charaDatas;
    }

    //private void Update()
    //{
    //    PlayerManager p = PlayerManager.Instance;
    //    CharacterData c;
    //
    //    for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
    //    {
    //        c = p.charaDatas[i];
    //        if (Input.GetButtonDown(playerInput[i].Option)) GameManager.Instance.isPause = !GameManager.Instance.isPause;
    //        if (c.isDeath == true) continue;
    //
    //        Vector3 moveInput = playerInput[i].MoveAxis;
    //        Vector3 viewMoveInput = playerInput[i].ViewPointMoveAxis;
    //
    //        if (moveInput.magnitude > 0.2f) characterMover.Move(moveInput, c);
    //        if (viewMoveInput.magnitude > 0.2f) characterMover.ViewMove(viewMoveInput, c);
    //
    //        if (Input.GetButtonDown(playerInput[i].Jump)) characterMover.Jump(c);
    //        if (Input.GetAxis(playerInput[i].SwitchToADS) > 0.2f) characterMover.ToADS(c);
    //        else characterMover.ToNonADS(c);
    //        if (Input.GetAxis(playerInput[i].PillowThrow) > 0.2f && c.remainthrowCT < 0 && c.isHavePillow) characterMover.PillowThrow(c);
    //    }
    //
    //    // ※テスト用※ キーボード操作
    //    if (isUseKeyboard == true) KeyboardMove();
    //}

    public void UpdateMethod()
    {

    }

    public void GeneralInputUpdateMethod()
    {
        for (int i = 0; i < gameManager.joinPlayers; i++)
        {
            if (Input.GetButtonDown(playerInput[i].Option)) gameManager.isPause = !gameManager.isPause;
        }
        if (isUseKeyboard == true) KeyboardMove();
    }

    public void UiInputUpdateMethod()
    {
        for (int i = 0; i < gameManager.joinPlayers; i++)
        {

        }
    }

    public void MoveInputUpdateMethpod()
    {
        for (int i = 0; i < gameManager.joinPlayers; i++)
        {
            if (characterDatas[i].isDeath == true) continue;

            Vector3 moveInput = playerInput[i].MoveAxis;
            Vector3 viewMoveInput = playerInput[i].ViewPointMoveAxis;

            if (moveInput.magnitude > 0.2f) characterMover.Move(moveInput, characterDatas[i]);
            if (viewMoveInput.magnitude > 0.2f) characterMover.ViewMove(viewMoveInput, characterDatas[i]);

            if (Input.GetButtonDown(playerInput[i].Jump)) characterMover.Jump(characterDatas[i]);
            if (Input.GetAxis(playerInput[i].SwitchToADS) > 0.2f) characterMover.ToADS(characterDatas[i]);
            else characterMover.ToNonADS(characterDatas[i]);
            if (Input.GetAxis(playerInput[i].PillowThrow) > 0.2f && characterDatas[i].remainthrowCT < 0 && characterDatas[i].isHavePillow) characterMover.PillowThrow(characterDatas[i]);
        }
    }

    private void KeyboardMove()
    {
        CharacterData c = characterDatas[keyboardMovePlayerId];
        if (c.isDeath == true) return;

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
