using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBedCollider : MonoBehaviour
{
    [SerializeField] private NpcBehaviorRoutine npcBehaviorRoutine;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bed") && npcBehaviorRoutine.characterData.bedStatus == null && npcBehaviorRoutine.npcStatus == NPC_STATUS.GO_BED)
        {
            Debug.Log("êNì¸");
            npcBehaviorRoutine.characterData.isInBedRange = true;
            npcBehaviorRoutine.characterData.inBedPos = other.transform.position;
            BedStatus bed = other.GetComponent<BedStatus>();
            npcBehaviorRoutine.characterData.bedStatus = bed;

            npcBehaviorRoutine.characterMover.InteractBed(npcBehaviorRoutine.characterData, true, npcBehaviorRoutine.characterData.inBedPos);
            npcBehaviorRoutine.InteractBed(true);
        }
    }
}
