using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MoveData‚Æ“¯‚¶ƒtƒ@ƒCƒ‹‚Å—Ç‚­‚È‚¢...?
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/RuleData", order = 3)]
public class RuleData : ScriptableObject
{
    public int maxHp;
    public int hitPillowCountOnDamage;
    public float pillowThrowCT;
    public float pillowHeadShotStunTime;
    public float inBedDamageTime;
}
