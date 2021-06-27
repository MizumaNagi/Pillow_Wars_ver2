using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/InputData", order = 1)]
public class InputData : ScriptableObject
{
    [SerializeField] private int playerNo;

    [Header("�Q�[��������")]
    [SerializeField] private XboxConAllTypeEnum moveX = XboxConAllTypeEnum.Xbox_Axis_L_Horizontal;          // �ړ� (���WX)
    [SerializeField] private XboxConAllTypeEnum moveY = XboxConAllTypeEnum.Xbox_Axis_L_Vertical;          // �ړ� (���WY)
    [SerializeField] private XboxConAllTypeEnum viewpointMoveX = XboxConAllTypeEnum.Xbox_Axis_R_Horizontal; // ���W�ړ� (���WX)
    [SerializeField] private XboxConAllTypeEnum viewpointMoveY = XboxConAllTypeEnum.Xbox_Axis_R_Vertical; // ���W�ړ� (���WY)
                                 
    [SerializeField] private XboxConAllTypeEnum run = XboxConAllTypeEnum.Xbox_Fire_L_Stick;           // ����
    [SerializeField] private XboxConAllTypeEnum squat = XboxConAllTypeEnum.Xbox_Fire_B;         // ���Ⴊ��
    [SerializeField] private XboxConAllTypeEnum jump = XboxConAllTypeEnum.Xbox_Fire_A;          // �W�����v
    [SerializeField] private XboxConAllTypeEnum interact = XboxConAllTypeEnum.Xbox_Fire_X;      // �C���^���N�g
    [SerializeField] private XboxConAllTypeEnum switchToADS = XboxConAllTypeEnum.Xbox_Trigger_LT;   // ADS��Ԃֈڍs
    [SerializeField] private XboxConAllTypeEnum pillowThrow = XboxConAllTypeEnum.Xbox_Trigger_RT;   // ������
    [SerializeField] private XboxConAllTypeEnum option = XboxConAllTypeEnum.Xbox_Fire_Menu;        // �I�v�V�������

    [Header("�Q�[���O����")]
    [SerializeField] private XboxConAllTypeEnum cursolMoveX = XboxConAllTypeEnum.Xbox_Axis_L_Horizontal;
    [SerializeField] private XboxConAllTypeEnum cursolMoveY = XboxConAllTypeEnum.Xbox_Axis_L_Vertical;
    //[SerializeField] private XboxConAllTypeEnum up = XboxConAllTypeEnum.Not_Use;      // ��ړ�
    //[SerializeField] private XboxConAllTypeEnum down = XboxConAllTypeEnum.Not_Use;    // ���ړ�
    //[SerializeField] private XboxConAllTypeEnum left = XboxConAllTypeEnum.Not_Use;    // ���ړ�
    //[SerializeField] private XboxConAllTypeEnum right = XboxConAllTypeEnum.Not_Use;   // �E�ړ�

    [SerializeField] private XboxConAllTypeEnum ok = XboxConAllTypeEnum.Xbox_Fire_A;      // ����
    [SerializeField] private XboxConAllTypeEnum cancel = XboxConAllTypeEnum.Xbox_Fire_B;  // �L�����Z��
    [SerializeField] private XboxConAllTypeEnum start = XboxConAllTypeEnum.Xbox_Fire_Menu;

    //[Header("�Q�[��������")]
    //[Header("�L�[�{�[�h����(�e�X�g�p)")]
    //[SerializeField] private KeyCode key_moveX;          // �ړ� (���WX)
    //[SerializeField] private KeyCode key_moveY;          // �ړ� (���WY)
    //[SerializeField] private string key_viewpointMoveX = ; // ���W�ړ� (���WX)
    //[SerializeField] private string key_viewpointMoveY = ; // ���W�ړ� (���WY)
    //
    //[SerializeField] private KeyCode key_run;           // ����
    //[SerializeField] private KeyCode key_squat;         // ���Ⴊ��
    //[SerializeField] private KeyCode key_jump;          // �W�����v
    //[SerializeField] private KeyCode key_interact;      // �C���^���N�g
    //[SerializeField] private KeyCode key_switchToADS;   // ADS��Ԃֈڍs
    //[SerializeField] private KeyCode key_pillowThrow;   // ������
    //[SerializeField] private KeyCode key_option;        // �I�v�V�������
    //
    //[Header("�Q�[���O����")]
    //[SerializeField] private KeyCode key_up_down;      // �㉺�ړ�
    //[SerializeField] private KeyCode key_left_right;   // ���E�ړ�
    //
    //[SerializeField] private KeyCode key_ok;           // ����
    //[SerializeField] private KeyCode key_cancel;       // �L�����Z��

    public string MoveX { get => GetKeyName(moveX); }
    public string MoveY { get => GetKeyName(moveY); }
    public Vector3 MoveAxis { get => GetAxisValue(MoveX, MoveY, 1, -1); }
    public string ViewpointMoveX { get => GetKeyName(viewpointMoveX); }
    public string ViewpointMoveY { get => GetKeyName(viewpointMoveY); }
    public Vector3 ViewPointMoveAxis { get => GetAxisValue(ViewpointMoveX, ViewpointMoveY, 1, -1); }
    public string Run { get => GetKeyName(run); }
    public string Squat { get => GetKeyName(squat); }
    public string Jump { get => GetKeyName(jump); }
    public string SwitchToADS { get => GetKeyName(switchToADS); }
    public string Interact { get => GetKeyName(interact); }
    public string PillowThrow { get => GetKeyName(pillowThrow); }

    public string Option { get => GetKeyName(option); }
    public string CursolMoveX { get => GetKeyName(cursolMoveX); }
    public string CursolMoveY { get => GetKeyName(cursolMoveY); }
    public Vector3 CursolMoveAxis { get => GetAxisValue(CursolMoveY, CursolMoveY, 1, 1); }

    //public string Up { get => GetKeyName(up); }
    //public string Down { get => GetKeyName(down); }
    //public string Left { get => GetKeyName(left); }
    //public string Right { get => GetKeyName(right); }
    public string Ok { get => GetKeyName(ok); }
    public string Cancel { get => GetKeyName(cancel); }

    public string Start { get => GetKeyName(start); }

    private string GetKeyName<T>(T key)
    {
        return $"{Enum.GetName(typeof(T), key)}_P{playerNo}";
    }

    private Vector3 GetAxisValue(string xAxisName, string yAxisName, int xAxisMulti, int yAxismulti)
    {
        Vector3 vec = Vector3.zero;
        vec.x = Input.GetAxis(xAxisName) * xAxisMulti;
        vec.z = Input.GetAxis(yAxisName) * yAxismulti;
        return vec;
    }
}
