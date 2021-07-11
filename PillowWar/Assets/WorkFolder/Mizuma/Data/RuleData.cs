using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MoveData�Ɠ����t�@�C���ŗǂ��Ȃ�...?
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/RuleData", order = 3)]
public class RuleData : ScriptableObject
{
    public int maxHp;
    public int hitPillowCountOnDamage;
    public float pillowThrowCT;
    public float inBedDamageTime;
    [Header("�w�b�h�V���b�g�֘A")]
    public float headShotBorderLocalPosY;
    public float stunRegistTime;
    public float pillowHeadShotStunTime;
    public float stunPercent;
}
