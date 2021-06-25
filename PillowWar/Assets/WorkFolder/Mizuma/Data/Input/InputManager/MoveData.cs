using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// RuleData�Ɠ����t�@�C���ŗǂ��̂ł�...�H
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/MoveData", order = 2)]
public class MoveData : ScriptableObject
{
    [Header("�ړ�����")]
    public float moveForce;
    public float walkMoveSpdLimit;
    public float squatMoveSpdLimit;
    public float dashMovMulti;
    public float viewMoveSpd;
    public float jumpForce;

    [Header("�}�N������")]
    public float throwForce;
    public float updateVelocityCoeffcient;
    public Vector3 pillowSpawnPos;

    [Header("�J��������")]
    public float minFOV;
    public float maxFOV;
    public float fovChangeSpd;
    public float limitRotY;
    public Vector3 standingCameraPos;
    public Vector3 squatingCameraPos;
}
