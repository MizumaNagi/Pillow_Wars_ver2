using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData
{
    public CharacterData(GameObject _myObject)
    {
        myObject = _myObject;
        Transform t = myObject.transform;
        myBodyTransform = t;
        myPillowTransform = t.GetChild(2).transform;
        myCameraTransform = t.GetChild(3).transform;
        myBodyRigidbody = myObject.GetComponent<Rigidbody>();
        myPillowRigidbody = t.GetChild(2).GetComponent<Rigidbody>();
        myCamera = t.GetChild(3).GetComponent<Camera>();
        hp = GameManager.Instance.ruleData.maxHp;
    }

    public GameObject myObject;
    public Transform myBodyTransform;
    public Transform myPillowTransform;
    public Transform myCameraTransform;
    public Rigidbody myBodyRigidbody;
    public Rigidbody myPillowRigidbody;
    public Camera myCamera;

    public int hp;
    public float remainthrowCT;
}
