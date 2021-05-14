using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData
{
    public CharacterData(GameObject _myObject)
    {
        character = _myObject;
        Transform t = character.transform;
        myBodyTransform = t;
        myPillowTransform = t.GetChild(3).transform;
        myCameraTransform = t.GetChild(2).transform;
        myBodyRigidbody = character.GetComponent<Rigidbody>();
        myPillowRigidbody = t.GetChild(3).GetComponent<Rigidbody>();
        myCamera = t.GetChild(2).GetComponent<Camera>();
        hp = GameManager.Instance.ruleData.maxHp;
    }

    public GameObject character;
    public GameObject myPillow;
    public Transform myBodyTransform;
    public Transform myPillowTransform;
    public Transform myCameraTransform;
    public Rigidbody myBodyRigidbody;
    public Rigidbody myPillowRigidbody;
    public Camera myCamera;

    public int hp;
    public float remainthrowCT = 0;
}
