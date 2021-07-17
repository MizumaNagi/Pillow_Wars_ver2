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

    public void EventStart()
    {
        int rnd = Random.Range(0, syojiChildCount - 1);
        syoujiParent.GetChild(rnd).GetComponent<DoorAnimation>().InteractDoor();
        transform.position = syoujiParent.GetChild(rnd).Find("TeacherPoint").gameObject.transform.position;
        transform.rotation = syoujiParent.GetChild(rnd).Find("TeacherPoint").gameObject.transform.rotation;
    }
}
