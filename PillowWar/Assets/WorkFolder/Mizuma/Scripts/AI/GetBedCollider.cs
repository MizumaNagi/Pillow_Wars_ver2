using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NPC���t�߂�BedStatus���擾���ABedStatus���̊֐����g���������������鏀��������N���X
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
        // �t�߂�Bed������A������NPC��Ԃ�"�x�b�h�Ɍ������Ă���"��
        if (other.gameObject.CompareTag("Bed") && npcBehaviorRoutine.npcStatus == NPC_STATUS.GO_BED)
        {
            // ���m���ŕz�c�ɓ��鎖�����s����
            float failedValue = npcBehaviorRoutine.routineData.failedInBedPercent;
            float rnd = Random.Range(0,100);
            if (failedValue > rnd)
            {
                npcBehaviorRoutine.SetNpcStatus(NPC_STATUS.WALK);
                return;
            }

            // �͈͓���BedStatus���擾����
            npcBehaviorRoutine.characterData.isInBedRange = true;
            npcBehaviorRoutine.characterData.inBedPos = other.transform.position;
            BedStatus bed = other.GetComponent<BedStatus>();
            npcBehaviorRoutine.characterData.bedStatus = bed;

            // �x�b�h�ɓ���
            npcBehaviorRoutine.characterMover.InteractBed(npcBehaviorRoutine.characterData, true, npcBehaviorRoutine.characterData.inBedPos);
            npcBehaviorRoutine.InteractBed(true);

            npcBehaviorRoutine.SetNpcStatus(NPC_STATUS.IN_BED);
        }
    }
}
