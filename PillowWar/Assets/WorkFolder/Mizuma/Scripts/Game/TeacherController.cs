using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherController : MonoBehaviour
{
    [SerializeField] private Transform syoujiParent;

    private int syojiChildCount = 0;

    private void Start()
    {
        syojiChildCount = syoujiParent.childCount;
    }

    private void Update()
    {
        if (GameEventScript.Instance.triggerEventEnd == true)
        {
            EventStart();
        }
    }

    public delegate void CallBack();
    public void EventStart()
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

    private IEnumerator DelayDoorAnimation(UnityEngine.Events.UnityAction callBack)
    {
        yield return new WaitForSeconds(3);

        callBack();
    }
}
