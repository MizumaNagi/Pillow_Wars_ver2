using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/NpcRoutineData", order = 4)]
public class NpcRoutineData : ScriptableObject
{
    public GameObject targetMark;
    public Vector3 negativeStageRange;
    public Vector3 positiveStageRange;

    [Range(1f, 5f)] public float searchNavMeshRange;        // ランダム座標から歩行可能なNavMeshを探し出す範囲
    [Range(1, 10)] public int searchMaxCount;               // ランダム座標が見つからなかった際に最大何回繰り返し検索するか
    public float distanceToEnemy;                           // 敵との接敵距離
    public float warRangeToEnemy;                           // 敵との戦闘距離
    public float maxSearchAngle;                            // 視野
    public float failedInBedPercent;                        // 目の前の空ベッドに入る動作を失敗する可能性
    public float startGoBedRemHpPercent;                    // ベッド潜り込みイベントを開始するHP割合
    public float minStartGoBedPercent;                      // ベッド潜り込みイベントの最低実行率
    public float shootingDamageChgTargetRoutinePercent;     // 射撃中に攻撃を受けた際にターゲットを変更する可能性
    public float shootingDamageChgEscapeRoutinePercent;     // 射撃中に攻撃を受けた際にターゲットから逃走する可能性
    public float shootingDamageChgEscapeAndJumpRoutinePercent; // 射撃中に攻撃を受けた際にターゲットから跳ねながら逃走する可能性
    public float minStartGoBedTime;                         // ベッドへ向かう最小襲来残り時間
    public float maxStartGoBedTime;                         // ベッドへ向かう最大襲来残り時間
}
