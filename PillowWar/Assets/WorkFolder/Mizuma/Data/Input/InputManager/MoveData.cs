using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// RuleDataと同じファイルで良いのでは...？
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/MoveData", order = 2)]
public class MoveData : ScriptableObject
{
    [Header("移動操作")]
    public float moveForce;
    public float walkMoveSpdLimit;
    public float squatMoveSpdLimit;
    public float dashMovMulti;
    public float viewMoveSpd;
    public float jumpForce;

    [Header("マクラ操作")]
    public float throwForce;
    public float throwAngle;
    public float throwAngleInBuff;
    public float throwMissVec;
    public float npcThrowMissVec;
    public float updateVelocityCoeffcient;
    public Vector3 pillowSpawnPos;
    public Vector3 pillowSpawnRot;

    [Header("カメラ操作")]
    public float minFOV;
    public float maxFOV;
    public float fovChangeSpd;
    public float limitRotY;
    public Vector3 standingCameraPos;
    public Vector3 squatingCameraPos;
}
