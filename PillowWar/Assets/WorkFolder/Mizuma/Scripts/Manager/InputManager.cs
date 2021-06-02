using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if (Input.GetButtonDown(playerInput[i].Option)) gameManager.isPause = !gameManager.isPause;
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
        for (int i = 0; i < gameManager.joinPlayers; i++)
        {
            if (characterDatas[i].isDeath == true) continue;
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
                if(characterDatas[i].isDash) characterMover.Move(moveInput * moveData.dashMovMulti, characterDatas[i]);
                else characterMover.Move(moveInput, characterDatas[i]);
            }
            else characterMover.Dash(characterDatas[i], false);

            // �_�b�V������
            if (Input.GetButtonDown(playerInput[i].Run))
            {
                characterMover.Dash(characterDatas[i], true);
            }
            // ���_�ړ�
            if (viewMoveInput.magnitude > 0.05f)
            {
                characterMover.ViewMove(viewMoveInput, characterDatas[i]);
            }
            // �W�����v
            if (Input.GetButtonDown(playerInput[i].Jump) && characterDatas[i].canJump == true)
            {
                characterMover.Jump(characterDatas[i]);
            }
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
                characterMover.PillowThrow(characterDatas[i]);
            }
            // �z�c�ɓ���
            if (Input.GetButtonDown(playerInput[i].Interact) && characterDatas[i].isInBedRange == true)
            {
                characterMover.InteractBed(characterDatas[i], true, characterDatas[i].inBedPos);
            }
            // �h�A�J��
            if (Input.GetButtonDown(playerInput[i].Interact) && characterDatas[i].isInDoor == true)
            {
                PlayerManager.Instance.charaDatas[i].doorAnimation.InteractDoor();
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
        if (c.isDeath == true) return;

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
        if (Input.GetKey(KeyCode.LeftShift)) characterMover.Dash(c, true);
        else characterMover.Dash(c, false);
        // �L�[�{�[�h-�W�����v
        if (Input.GetKeyDown(KeyCode.Space) && c.canJump == true) characterMover.Jump(c);
        // �L�[�{�[�h-ADS/��ADS
        if (Input.GetMouseButton(1)) { characterMover.ToADS(c); }
        else { characterMover.ToNonADS(c); }
        // �L�[�{�[�h-������
        if (Input.GetMouseButton(0) && c.isHavePillow) characterMover.PillowThrow(c);
        // �L�[�{�[�h-�z�c�ɓ���
        if (Input.GetKeyDown(KeyCode.E) && c.isInBedRange == true) characterMover.InteractBed(c, true, c.inBedPos);
        // �L�[�{�[�h-�h�A�J��
        if (Input.GetKeyDown(KeyCode.E) && c.isInDoor == true) PlayerManager.Instance.charaDatas[keyboardMovePlayerId].doorAnimation.InteractDoor();
    }

    private void KeyboardInputMove(CharacterData c)
    {
        float spdMulti = (c.isDash == true) ? moveData.dashMovMulti : 1;

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
