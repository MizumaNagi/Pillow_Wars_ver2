using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MoveDataと同じファイルで良くない...?
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/RuleData", order = 3)]
public class RuleData : ScriptableObject
{
    public int maxHp;
    public int hitPillowCountOnDamage;
    public float pillowThrowCT;
    public float inBedDamageTime;
    [Header("ヘッドショット関連")]
    public float headShotBorderLocalPosY;
    public float stunRegistTime;
    public float pillowHeadShotStunTime;
    public float stunPercent;
}
