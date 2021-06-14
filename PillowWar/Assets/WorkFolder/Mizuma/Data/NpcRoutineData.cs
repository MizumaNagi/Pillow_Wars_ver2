using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/NpcRoutineData", order = 4)]
public class NpcRoutineData : ScriptableObject
{
    public GameObject targetMark;
    public Vector3 stageRange;
    [Range(1f, 5f)] public float searchNavMeshRange;
    public float warRangeWithEnemy;
    public float maxSearchAngle;
}
