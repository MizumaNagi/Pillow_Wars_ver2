using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// RuleDataと同じファイルで良いのでは...？
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/MoveData", order = 2)]
public class MoveData : ScriptableObject
{
    [Header("移動操作")]
    public float moveSpd;
    public float dashMovMulti;
    public float viewMoveSpd;
    public float jumpForce;

    [Header("マクラ操作")]
    public float throwForce;
    public Vector3 pillowSpawnPos;

    [Header("カメラ操作")]
    public float minFOV;
    public float maxFOV;
    public float fovChangeSpd;
    public float limitRotY;
}
