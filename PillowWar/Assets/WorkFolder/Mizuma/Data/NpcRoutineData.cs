using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/NpcRoutineData", order = 4)]
public class NpcRoutineData : ScriptableObject
{
    public GameObject targetMark;
    public Vector3 stageRange;

    [Range(1f, 5f)] public float searchNavMeshRange;        // ランダム座標から歩行可能なNavMeshを探し出す範囲
    public float distanceToEnemy;                           // 敵との接敵距離
    public float warRangeToEnemy;                           // 敵との戦闘距離
    public float maxSearchAngle;                            // 視野
    public float startGoBedRemHpPercent;                    // ベッド潜り込みイベントを開始するHP割合
    public float minStartGoBedPercent;                      // ベッド潜り込みイベントの最低実行率
    public float shootingDamageChgTargetRoutinePercent;     // 射撃中に攻撃を受けた際にターゲットを変更する可能性
    public float shootingDamageChgEscapeRoutinePercent;     // 射撃中に攻撃を受けた際にターゲットから逃走する可能性
    public float shootingDamageChgEscapeAndJumpRoutinePercent; // 射撃中に攻撃を受けた際にターゲットから跳ねながら逃走する可能性
}
