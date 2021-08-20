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
        // �I�v�V�������
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
                // �z�c����o��
                if (Input.GetButtonDown(playerInput[i].Interact))
                {
                    characterMover.InteractBed(characterDatas[i], false, characterDatas[i].inBedPos);
                }
                continue;
            }

            Vector3 moveInput = playerInput[i].MoveAxis;
            Vector3 viewMoveInput = playerInput[i].ViewPointMoveAxis;

            // �ړ�/�_�b�V����������
            if (moveInput.magnitude > 0.2f)
            {
                if (characterDatas[i].isDash) characterMover.Move(moveInput * moveData.dashMovMulti, characterDatas[i]);
                else characterMover.Move(moveInput, characterDatas[i]);
            }
            else characterMover.Dash(characterDatas[i], false);

            // �_�b�V������
            if (Input.GetButtonDown(playerInput[i].Run) && characterDatas[i].isSquat == false)
            {
                characterMover.Dash(characterDatas[i], true);
            }
            // ���_�ړ�
            if (viewMoveInput.magnitude > 0.05f)
            {
                characterMover.ViewMove(viewMoveInput, characterDatas[i]);
            }
            // �W�����v
            if (Input.GetButtonDown(playerInput[i].Jump) && characterDatas[i].canJump == true && characterDatas[i].isSquat == false)
            {
                characterMover.Jump(characterDatas[i]);
            }
            // ���Ⴊ��
            //if (Input.GetButtonDown(playerInput[i].Squat) && characterDatas[i].canJump == true)
            //{
            //    characterMover.Squat(characterDatas[i], characterDatas[i].isSquat);
            //}
            // ADS/��ADS
            if (Input.GetAxis(playerInput[i].SwitchToADS) > 0.2f)
            {
                characterMover.ToADS(characterDatas[i]);
            }
            else
            {
                characterMover.ToNonADS(characterDatas[i]);
            }
            // ������
            if (Input.GetAxis(playerInput[i].PillowThrow) > 0.2f && characterDatas[i].remainthrowCT < 0 && characterDatas[i].isHavePillow)
            {
                characterMover.PillowThrow(characterDatas[i], false);
            }
            // �z�c�ɓ���
            if (Input.GetButtonDown(playerInput[i].Interact) && characterDatas[i].isInBedRange == true && GameEventScript.Instance.canBedIn == true)
            {
                characterMover.InteractBed(characterDatas[i], true, characterDatas[i].inBedPos);
            }
        }

        if (isUseKeyboard == true) KeyboardMoveInputUpdateMethod();
    }

    // ���e�X�g�p�� �L�[�{�[�h����
    private void KeyboardGeneralInputMethod()
    {
        // �L�[�{�[�h-�I�v�V�������
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
            // �L�[�{�[�h-�z�c����o��
            if (Input.GetKeyDown(KeyCode.E)) characterMover.InteractBed(c, false, c.inBedPos);
            return;
        }

        // �L�[�{�[�h-���_�ړ�
        KeyboardInputViewMove(c);

        if (Input.anyKey == false) return;
        // �L�[�{�[�h-�ړ�
        KeyboardInputMove(c);
        // �L�[�{�[�h-�_�b�V������/��������
        if (Input.GetKey(KeyCode.LeftShift) && c.isSquat == false) characterMover.Dash(c, true);
        else characterMover.Dash(c, false);
        // �L�[�{�[�h-�W�����v
        if (Input.GetKeyDown(KeyCode.Space) && c.canJump == true && c.isSquat == false) characterMover.Jump(c);
        // �L�[�{�[�h-���Ⴊ��
        //if (Input.GetKeyDown(KeyCode.LeftControl) && c.canJump == true) characterMover.Squat(c, c.isSquat);
        // �L�[�{�[�h-ADS/��ADS
        if (Input.GetMouseButton(1)) { characterMover.ToADS(c); }
        else { characterMover.ToNonADS(c); }
        // �L�[�{�[�h-������
        if (Input.GetMouseButton(0) && c.isHavePillow && c.remainthrowCT < 0) characterMover.PillowThrow(c, false);
        // �L�[�{�[�h-�z�c�ɓ���
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

