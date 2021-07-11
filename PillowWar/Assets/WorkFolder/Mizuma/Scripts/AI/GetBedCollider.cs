using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NPCが付近のBedStatusを取得し、BedStatus内の関数を使い睡眠処理をする準備をするクラス
/// </summary>
public class GetBedCollider : MonoBehaviour
{
    [SerializeField] private NpcBehaviorRoutine npcBehaviorRoutine;

    private void Update()
    {
        if (GameManager.Instance.isPause == true) return;
    }

    private void OnTriggerStay(Collider other)
    {
        // 付近にBedがあり、尚且つNPC状態が"ベッドに向かっている"時
        if (other.gameObject.CompareTag("Bed") && npcBehaviorRoutine.npcStatus == NPC_STATUS.GO_BED)
        {
            // 一定確率で布団に入る事を失敗する
            float failedValue = npcBehaviorRoutine.routineData.failedInBedPercent;
            float rnd = Random.Range(0,100);
            if (failedValue > rnd)
            {
                npcBehaviorRoutine.SetNpcStatus(NPC_STATUS.WALK);
                return;
            }

            // 範囲内のBedStatusを取得する
            npcBehaviorRoutine.characterData.isInBedRange = true;
            npcBehaviorRoutine.characterData.inBedPos = other.transform.position;
            BedStatus bed = other.GetComponent<BedStatus>();
            npcBehaviorRoutine.characterData.bedStatus = bed;

            // ベッドに入る
            npcBehaviorRoutine.characterMover.InteractBed(npcBehaviorRoutine.characterData, true, npcBehaviorRoutine.characterData.inBedPos);
            npcBehaviorRoutine.InteractBed(true);

            npcBehaviorRoutine.SetNpcStatus(NPC_STATUS.IN_BED);
        }
    }
}
