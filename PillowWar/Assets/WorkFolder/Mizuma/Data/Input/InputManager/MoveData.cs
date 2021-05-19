using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// RuleData�Ɠ����t�@�C���ŗǂ��̂ł�...�H
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/MoveData", order = 2)]
public class MoveData : ScriptableObject
{
    [Header("�ړ�����")]
    public float moveSpd;
    public float dashMovMulti;
    public float viewMoveSpd;
    public float jumpForce;

    [Header("�}�N������")]
    public float throwForce;
    public Vector3 pillowSpawnPos;

    [Header("�J��������")]
    public float minFOV;
    public float maxFOV;
    public float fovChangeSpd;
    public float limitRotY;
}
