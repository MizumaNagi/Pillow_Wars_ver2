using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InputManager : SingletonMonoBehaviour<InputManager>
{
    [SerializeField] private bool isUseKeyboard = false;
    [SerializeField] private int keyboardMovePlayerId;

    public InputData[] playerInput;

    private CharacterMover characterMover = new CharacterMover();
    private GameManager gameManager;

    public MoveData moveData;
    public List<CharacterData> characterDatas = new List<CharacterData>();

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void GeneralInputUpdateMethod()
    {
        // オプション画面
        for (int i = 0; i < gameManager.joinPlayers; i++)
        {
            if (i == keyboardMovePlayerId && isUseKeyboard) continue;
            if (Input.GetButtonDown(playerInput[i].Option))
            {
                Time.timeScale = gameManager.isPause ? 1 : 0;
                gameManager.isPause = !gameManager.isPause;
            }
        }

        if (isUseKeyboard == true) KeyboardGeneralInputMethod();
    }

    public void UiInputUpdateMethod()
    {
        for (int i = 0; i < gameManager.joinPlayers; i++)
        {
            if (i == keyboardMovePlayerId && isUseKeyboard) continue;
        }

        if (isUseKeyboard == true) KeyboardUiInputUpdateMethod();
    }

    public void MoveInputUpdateMethod()
    {
        if (GameEventScript.Instance.canAction == false) return;

        for (int i = 0; i < gameManager.joinPlayers; i++)
        {
            if (characterDatas[i].isDeath == true || characterDatas[i].remainStunTime > 0) continue;
            if (i == keyboardMovePlayerId && isUseKeyboard) continue;

            if (characterDatas[i].isInBed == true)
            {
                // 布団から出る
                if (Input.GetButtonDown(playerInput[i].Interact))
                {
                    characterMover.InteractBed(characterDatas[i], false, characterDatas[i].inBedPos);
                }
                continue;
            }

            Vector3 moveInput = playerInput[i].MoveAxis;
            Vector3 viewMoveInput = playerInput[i].ViewPointMoveAxis;

            // 移動/ダッシュ解除判定
            if (moveInput.magnitude > 0.2f)
            {
                if (characterDatas[i].isDash) characterMover.Move(moveInput * moveData.dashMovMulti, characterDatas[i]);
                else characterMover.Move(moveInput, characterDatas[i]);
            }
            else characterMover.Dash(characterDatas[i], false);

            // ダッシュ判定
            if (Input.GetButtonDown(playerInput[i].Run) && characterDatas[i].isSquat == false)
            {
                characterMover.Dash(characterDatas[i], true);
            }
            // 視点移動
            if (viewMoveInput.magnitude > 0.05f)
            {
                characterMover.ViewMove(viewMoveInput, characterDatas[i]);
            }
            // ジャンプ
            if (Input.GetButtonDown(playerInput[i].Jump) && characterDatas[i].canJump == true && characterDatas[i].isSquat == false)
            {
                characterMover.Jump(characterDatas[i]);
            }
            // しゃがみ
            //if (Input.GetButtonDown(playerInput[i].Squat) && characterDatas[i].canJump == true)
            //{
            //    characterMover.Squat(characterDatas[i], characterDatas[i].isSquat);
            //}
            // ADS/非ADS
            if (Input.GetAxis(playerInput[i].SwitchToADS) > 0.2f)
            {
                characterMover.ToADS(characterDatas[i]);
            }
            else
            {
                characterMover.ToNonADS(characterDatas[i]);
            }
            // 枕投げ
            if (Input.GetAxis(playerInput[i].PillowThrow) > 0.2f && characterDatas[i].remainthrowCT < 0 && characterDatas[i].isHavePillow)
            {
                characterMover.PillowThrow(characterDatas[i], false);
            }
            // 布団に入る
            if (Input.GetButtonDown(playerInput[i].Interact) && characterDatas[i].isInBedRange == true && GameEventScript.Instance.canBedIn == true)
            {
                characterMover.InteractBed(characterDatas[i], true, characterDatas[i].inBedPos);
            }
        }

        if (isUseKeyboard == true) KeyboardMoveInputUpdateMethod();
    }

    // ※テスト用※ キーボード操作
    private void KeyboardGeneralInputMethod()
    {
        // キーボード-オプション画面
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameManager.isPause = !gameManager.isPause;
        }
    }

    private void KeyboardUiInputUpdateMethod()
    {

    }

    private void KeyboardMoveInputUpdateMethod()
    {
        CharacterData c = characterDatas[keyboardMovePlayerId];
        if (c.isDeath == true || c.remainStunTime > 0) return;

        if (c.isInBed == true)
        {
            // キーボード-布団から出る
            if (Input.GetKeyDown(KeyCode.E)) characterMover.InteractBed(c, false, c.inBedPos);
            return;
        }

        // キーボード-視点移動
        KeyboardInputViewMove(c);

        if (Input.anyKey == false) return;
        // キーボード-移動
        KeyboardInputMove(c);
        // キーボード-ダッシュ判定/解除判定
        if (Input.GetKey(KeyCode.LeftShift) && c.isSquat == false) characterMover.Dash(c, true);
        else characterMover.Dash(c, false);
        // キーボード-ジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && c.canJump == true && c.isSquat == false) characterMover.Jump(c);
        // キーボード-しゃがみ
        //if (Input.GetKeyDown(KeyCode.LeftControl) && c.canJump == true) characterMover.Squat(c, c.isSquat);
        // キーボード-ADS/非ADS
        if (Input.GetMouseButton(1)) { characterMover.ToADS(c); }
        else { characterMover.ToNonADS(c); }
        // キーボード-枕投げ
        if (Input.GetMouseButton(0) && c.isHavePillow && c.remainthrowCT < 0) characterMover.PillowThrow(c, false);
        // キーボード-布団に入る
        if (Input.GetKeyDown(KeyCode.E) && c.isInBedRange == true && GameEventScript.Instance.canBedIn == true) characterMover.InteractBed(c, true, c.inBedPos);
    }

    private void KeyboardInputMove(CharacterData c)
    {
        float spdMulti = c.isDash == true ? moveData.dashMovMulti : 1;

        if (Input.GetKey(KeyCode.W)) characterMover.Move(spdMulti * Vector3.forward, c);
        if (Input.GetKey(KeyCode.A)) characterMover.Move(spdMulti * Vector3.left, c);
        if (Input.GetKey(KeyCode.S)) characterMover.Move(spdMulti * Vector3.back, c);
        if (Input.GetKey(KeyCode.D)) characterMover.Move(spdMulti * Vector3.right, c);
    }

    private void KeyboardInputViewMove(CharacterData c)
    {
        Vector3 vec = new Vector3(Input.GetAxis("Mouse X"), 0f, Input.GetAxis("Mouse Y"));
        characterMover.ViewMove(vec, c);
    }
}

