using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherController : MonoBehaviour
{
    [SerializeField] private Transform syoujiParent;
    [SerializeField] private Transform roukaViewPointParent;
    [SerializeField] private Animator teacherAnimator;

    private int syojiChildCount = 0;
    private int nextEventDoorIndex = 0;

    private void Start()
    {
        syojiChildCount = syoujiParent.childCount;
    }

    private void Update()
    {
        if (GameEventScript.Instance.triggerEventEnd == true)
        {
            if (GameManager.Instance.selectStageNo == 0) DoorEventStart();
            else StartCoroutine(DelayStartAnimation(PlayerManager.Instance.playerDatas[0].cameraController.arriveTime));
        }
    }

    public void ReadyNextEvent()
    {
        if(GameManager.Instance.selectStageNo == 0)
        {
            nextEventDoorIndex = Random.Range(0, syojiChildCount - 1);
            var effectControll = syoujiParent.GetChild(nextEventDoorIndex).GetComponent<DotaEffectVisibilityControll>();
            effectControll.ChgVisibilityEffect(true);
        }
        else
        {
            nextEventDoorIndex = Random.Range(0, 2);
            var spotEffectVisibility = GetComponent<SpotDotaEffectVisibility>();
            spotEffectVisibility.ChgVisibilityEffect(true, nextEventDoorIndex);
        }
    }

    public delegate void CallBack();
    public void DoorEventStart()
    {
        var doorAnimation = syoujiParent.GetChild(nextEventDoorIndex).GetComponent<DoorAnimation>();
        doorAnimation.InteractDoor();

        transform.position = syoujiParent.GetChild(nextEventDoorIndex).Find("TeacherPoint").gameObject.transform.position;
        transform.rotation = syoujiParent.GetChild(nextEventDoorIndex).Find("TeacherPoint").gameObject.transform.rotation;

        Vector3 cameraEndPos = syoujiParent.GetChild(nextEventDoorIndex).Find("CameraPoint").gameObject.transform.position;
        Quaternion cameraEndRot = syoujiParent.GetChild(nextEventDoorIndex).Find("CameraPoint").gameObject.transform.rotation;

        foreach (var playerData in PlayerManager.Instance.playerDatas)
        {
            if (playerData.isInBed == true) continue;
            StartCoroutine(playerData.cameraController.StartMoveCorutine(cameraEndPos, cameraEndRot));
        }

        StartCoroutine(DelayDoorAnimation(doorAnimation.InteractDoor));
    }

    private IEnumerator DelayStartAnimation(float waitTime)
    {

        foreach (var playerData in PlayerManager.Instance.playerDatas)
        {
            if (playerData.isInBed == true) continue;
            StartCoroutine(playerData.cameraController.StartMoveCorutine(roukaViewPointParent.GetChild(nextEventDoorIndex).position, roukaViewPointParent.GetChild(nextEventDoorIndex).rotation));
        }
        yield return new WaitForSeconds(waitTime);
        teacherAnimator.SetInteger("NextRndSpot", nextEventDoorIndex);
        teacherAnimator.SetTrigger("startEvent");
    }

    private IEnumerator DelayDoorAnimation(UnityEngine.Events.UnityAction callBack)
    {
        yield return new WaitForSeconds(3);

        callBack();
    }
}
