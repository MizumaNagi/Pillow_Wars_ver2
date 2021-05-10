using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/MoveData", order = 2)]
public class MoveData : ScriptableObject
{
    public float moveSpd;
    public float viewMoveSpd;
    public float jumpForce;
    public float pillowThrowCT;
    public float pillowHeadShotStun;
}
