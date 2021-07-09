using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventScript : SingletonMonoBehaviour<GameEventScript>
{
    [SerializeField] private GameEventData gameEventData;

    private int detailEventsNum;
    public int finishEventsNum;
    private bool isEventStart;
    private bool npcGoBedTrigger;
    private EVENT_TYPE nextEventType;
    private float npcGoBedTriggerRemTime = 10f;

    public List<NpcBehaviorRoutine> npcBehaviorRoutines = new List<NpcBehaviorRoutine>();
    public float remainEventStopTime;
    public float remainEventActiveTime;
    public bool canBedIn = false;


    private void Start()
    {
        detailEventsNum = gameEventData.gameEvents.Length;
    }

    public void Init()
    {
        NextEventStart();

    }

    public void UpdateMethod()
    {
        if (remainEventStopTime < remainEventActiveTime)
        {
            // �C�x���g�X�^�[�g�g���K�[
            if (isEventStart == false)
            {
                isEventStart = true;
            }
            remainEventActiveTime -= Time.deltaTime;

            // �z�c���荞�݃g���K�[
            if (remainEventActiveTime < npcGoBedTriggerRemTime && npcGoBedTrigger == false)
            {
                npcGoBedTrigger = true;
                // NPC�S���ɕz�c�i�s�g���K�[ (HP����=���s�m�� ����)
                // foreach (var npcBehaviorRoutine in npcBehaviorRoutines.ToArray())
                // {
                //     if (npcBehaviorRoutine.gameObject.activeSelf == true) npcBehaviorRoutine.TriggerGoBed();
                // }
            }

            if (npcBehaviorRoutines.Count != 0)
            {
                // �eNPC���z�c�i�s�\���ԂȂ�z�c��ڎw��
                foreach (var npcBehaviourRoutine in npcBehaviorRoutines.ToArray())
                {
                    if (npcBehaviourRoutine.gameObject.activeSelf == true)
                    {
                        if (npcBehaviourRoutine.startGoBedTime > remainEventActiveTime) npcBehaviourRoutine.CheckTimeTriggerGoBed();
                    }
                }
            }

            // �C�x���g����
            if (remainEventActiveTime < 0)
            {
                isEventStart = false;
                npcGoBedTrigger = false;
                NextEventStart();
                EventActive(nextEventType);

                // NPC�S���ɕz�c�i�s�g���K�[ (HP����=���s���� ����)
                if (npcBehaviorRoutines.Count != 0)
                {
                    foreach (var npcBehaviourRoutine in npcBehaviorRoutines.ToArray())
                    {
                        if (npcBehaviourRoutine.gameObject.activeSelf == true) npcBehaviourRoutine.ResetBedEventStatus();
                    }
                }
            }
            canBedIn = true;
        }
        else
        {
            canBedIn = false;
        }

        remainEventStopTime -= Time.deltaTime;
    }

    private void NextEventStart()
    {
        if (finishEventsNum <= detailEventsNum)
        {
            remainEventStopTime = gameEventData.gameEvents[finishEventsNum].stopEventInterval;
            remainEventActiveTime = gameEventData.gameEvents[finishEventsNum].eventActiveTime;
            nextEventType = gameEventData.gameEvents[finishEventsNum].type;
        }
        else
        {
            remainEventStopTime = gameEventData.finalEventInfo.stopEventInterval;
            remainEventActiveTime = gameEventData.finalEventInfo.eventActiveTime;
            nextEventType = gameEventData.finalEventInfo.type;
        }

        finishEventsNum++;
    }

    private void EventActive(EVENT_TYPE type)
    {
        if (type == EVENT_TYPE.TeacherAttack)
        {
            for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
            {
                PlayerManager.Instance.playerDatas[i].Damage(false, true);
            }

            for (int i = 0; i < GameManager.Instance.joinNpcs; i++)
            {
                PlayerManager.Instance.npcDatas[i].Damage(false, true);
                foreach (var npc in npcBehaviorRoutines.ToArray())
                {
                    if (npc.gameObject.activeSelf == true)
                    {
                        npc.StandUpBed();
                        npc.SetNpcStatus(NPC_STATUS.WALK);
                    }
                }
            }
        }
    }

    public void StatusReset()
    {
        finishEventsNum = 0;
        npcBehaviorRoutines.Clear();
    }
}