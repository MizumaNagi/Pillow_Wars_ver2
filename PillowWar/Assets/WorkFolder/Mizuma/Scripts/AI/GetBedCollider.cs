using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NPC���t�߂�BedStatus���擾���ABedStatus���̊֐����g���������������鏀��������N���X
/// </summary>
public class GetBedCollider : MonoBehaviour
{
    [SerializeField] private NpcBehaviorRoutine npcBehaviorRoutine;

    private void OnTriggerEnter(Collider other)
    {
        // �t�߂�Bed������A������NPC��Ԃ�"�x�b�h�Ɍ������Ă���"��
        if (other.gameObject.CompareTag("Bed") && npcBehaviorRoutine.npcStatus == NPC_STATUS.GO_BED)
        {
            // �͈͓���BedStatus���擾����
            Debug.Log("�N��");
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
