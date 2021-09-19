using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherController : MonoBehaviour
{
    [SerializeField] private Transform syoujiParent;
    [SerializeField] private Transform roukaViewPoint;
    [SerializeField] private Animator teacherAnimator;

    private int syojiChildCount = 0;

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

    public delegate void CallBack();
    public void DoorEventStart()
    {
        //int rnd = Random.Range(0, syojiChildCount - 1);
        int rnd = 2;
        var doorAnimation = syoujiParent.GetChild(rnd).GetComponent<DoorAnimation>();
        doorAnimation.InteractDoor();

        transform.position = syoujiParent.GetChild(rnd).Find("TeacherPoint").gameObject.transform.position;
        transform.rotation = syoujiParent.GetChild(rnd).Find("TeacherPoint").gameObject.transform.rotation;

        foreach(var playerData in PlayerManager.Instance.playerDatas)
        {
            if (playerData.isInBed == true) continue;

            Vector3 cameraEndPos = syoujiParent.GetChild(rnd).Find("CameraPoint").gameObject.transform.position;
            Quaternion cameraEndRot = syoujiParent.GetChild(rnd).Find("CameraPoint").gameObject.transform.rotation;

            StartCoroutine(playerData.cameraController.StartMoveCorutine(cameraEndPos, cameraEndRot));
        }

        StartCoroutine(DelayDoorAnimation(doorAnimation.InteractDoor));
    }

    private IEnumerator DelayStartAnimation(float waitTime)
    {
        foreach (var playerData in PlayerManager.Instance.playerDatas)
        {
            if (playerData.isInBed == true) continue;
            StartCoroutine(playerData.cameraController.StartMoveCorutine(roukaViewPoint.position, roukaViewPoint.rotation));
        }
        yield return new WaitForSeconds(waitTime);
        teacherAnimator.SetTrigger("startEvent");
    }

    private IEnumerator DelayDoorAnimation(UnityEngine.Events.UnityAction callBack)
    {
        yield return new WaitForSeconds(3);

        callBack();
    }
}
