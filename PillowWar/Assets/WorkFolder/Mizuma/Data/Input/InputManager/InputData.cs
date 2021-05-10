using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/InputData", order = 1)]
public class InputData : ScriptableObject
{
    [SerializeField] private int playerNo;

    [Header("�Q�[��������")]
    [SerializeField] private XboxConAllTypeEnum moveX = XboxConAllTypeEnum.Not_Use;          // �ړ� (���WX)
    [SerializeField] private XboxConAllTypeEnum moveY = XboxConAllTypeEnum.Not_Use;          // �ړ� (���WY)
    [SerializeField] private XboxConAllTypeEnum viewpointMoveX = XboxConAllTypeEnum.Not_Use; // ���W�ړ� (���WX)
    [SerializeField] private XboxConAllTypeEnum viewpointMoveY = XboxConAllTypeEnum.Not_Use; // ���W�ړ� (���WY)
                                 
    [SerializeField] private XboxConAllTypeEnum run = XboxConAllTypeEnum.Not_Use;           // ����
    [SerializeField] private XboxConAllTypeEnum squat = XboxConAllTypeEnum.Not_Use;         // ���Ⴊ��
    [SerializeField] private XboxConAllTypeEnum jump = XboxConAllTypeEnum.Not_Use;          // �W�����v
    [SerializeField] private XboxConAllTypeEnum interact = XboxConAllTypeEnum.Not_Use;      // �C���^���N�g
    [SerializeField] private XboxConAllTypeEnum switchToADS = XboxConAllTypeEnum.Not_Use;   // ADS��Ԃֈڍs
    [SerializeField] private XboxConAllTypeEnum pillowThrow = XboxConAllTypeEnum.Not_Use;   // ������
    [SerializeField] private XboxConAllTypeEnum option = XboxConAllTypeEnum.Not_Use;        // �I�v�V�������

    [Header("�Q�[���O����")]
    [SerializeField] private XboxConAllTypeEnum cursolMoveX = XboxConAllTypeEnum.Not_Use;
    [SerializeField] private XboxConAllTypeEnum cursolMoveY = XboxConAllTypeEnum.Not_Use;
    //[SerializeField] private XboxConAllTypeEnum up = XboxConAllTypeEnum.Not_Use;      // ��ړ�
    //[SerializeField] private XboxConAllTypeEnum down = XboxConAllTypeEnum.Not_Use;    // ���ړ�
    //[SerializeField] private XboxConAllTypeEnum left = XboxConAllTypeEnum.Not_Use;    // ���ړ�
    //[SerializeField] private XboxConAllTypeEnum right = XboxConAllTypeEnum.Not_Use;   // �E�ړ�

    [SerializeField] private XboxConAllTypeEnum ok = XboxConAllTypeEnum.Not_Use;      // ����
    [SerializeField] private XboxConAllTypeEnum cancel = XboxConAllTypeEnum.Not_Use;  // �L�����Z��

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
    public string ViewpointMoveX { get => GetKeyName(viewpointMoveX); }
    public string ViewpointMoveY { get => GetKeyName(viewpointMoveY); }
    public string Run { get => GetKeyName(run); }
    public string Squat { get => GetKeyName(squat); }
    public string Jump { get => GetKeyName(jump); }
    public string SwitchToADS { get => GetKeyName(switchToADS); }
    public string Interact { get => GetKeyName(interact); }
    public string PillowThrow { get => GetKeyName(pillowThrow); }

    public string Option { get => GetKeyName(option); }
    public string CursolMoveX { get => GetKeyName(cursolMoveX); }
    public string CursolMoveY { get => GetKeyName(cursolMoveY); }
    //public string Up { get => GetKeyName(up); }
    //public string Down { get => GetKeyName(down); }
    //public string Left { get => GetKeyName(left); }
    //public string Right { get => GetKeyName(right); }
    public string Ok { get => GetKeyName(ok); }
    public string Cancel { get => GetKeyName(cancel); }

    private string GetKeyName<T>(T key)
    {
        return $"{Enum.GetName(typeof(T), key)}_P{this.playerNo}";
    }
}
