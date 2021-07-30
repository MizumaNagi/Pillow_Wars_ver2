using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventScript : SingletonMonoBehaviour<GameEventScript>
{
    [SerializeField] private GameEventData gameEventData;

    private int detailEventsNum;
    public int finishEventsNum;
    public bool isEventStart;
    public bool triggerEventEnd = false;
    private bool npcGoBedTrigger;
    private EVENT_TYPE nextEventType;
    private float npcGoBedTriggerRemTime = 10f;

    public List<NpcBehaviorRoutine> npcBehaviorRoutines = new List<NpcBehaviorRoutine>();
    public float remainEventStopTime;
    public float remainEventActiveTime;
    public bool canBedIn = false;
    public bool canAction = true;

    public Vector3 openDoorCameraPos;
    public Quaternion openDoorCamerarot;

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
            // イベントスタートトリガー
            if (isEventStart == false)
            {
                isEventStart = true;
            }
        }
        if(isEventStart == true)
        { 
            remainEventActiveTime -= Time.deltaTime;

            // 布団潜り込みトリガー
            if (remainEventActiveTime < npcGoBedTriggerRemTime && npcGoBedTrigger == false)
            {
                npcGoBedTrigger = true;
                // NPC全員に布団進行トリガー (HP割合=実行確立 方式)
                // foreach (var npcBehaviorRoutine in npcBehaviorRoutines.ToArray())
                // {
                //     if (npcBehaviorRoutine.gameObject.activeSelf == true) npcBehaviorRoutine.TriggerGoBed();
                // }
            }

            if (npcBehaviorRoutines.Count != 0)
            {
                // 各NPCが布団進行可能時間なら布団を目指す
                foreach (var npcBehaviourRoutine in npcBehaviorRoutines.ToArray())
                {
                    if (npcBehaviourRoutine.gameObject.activeSelf == true)
                    {
                        if (npcBehaviourRoutine.startGoBedTime > remainEventActiveTime) npcBehaviourRoutine.CheckTimeTriggerGoBed();
                    }
                }
            }

            // イベント発生
            if (remainEventActiveTime < 0)
            {
                canAction = false;
                isEventStart = false;
                npcGoBedTrigger = false;
                NextEventStart();
                EventActive(nextEventType);
                TriggerEventBool();

                // NPC全員に布団進行トリガー (HP割合=実行時間 方式)
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

        if(canAction) remainEventStopTime -= Time.deltaTime;
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
        isEventStart = false;
        triggerEventEnd = false;
        canBedIn = false;
        canAction = true;
        npcBehaviorRoutines.Clear();
    }

    private void TriggerEventBool()
    {
        triggerEventEnd = true;
        StartCoroutine(BoolChangeOneFrameWait(false));
    }

    private IEnumerator BoolChangeOneFrameWait(bool isTrue)
    {
        yield return new WaitForEndOfFrame();
        triggerEventEnd = isTrue;
    }
}