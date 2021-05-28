using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    public CharacterData(GameObject _myObject, int _playerID)
    {
        character = _myObject;
        Transform t = character.transform;
        myBodyTransform = t;
        myPillowTransform = t.GetChild(3).transform;
        myCameraTransform = t.GetChild(2).transform;
        myBodyRigidbody = character.GetComponent<Rigidbody>();
        myPillowRigidbody = t.GetChild(3).GetComponent<Rigidbody>();
        myCamera = t.GetChild(2).GetComponent<Camera>();
        HP = GameManager.Instance.ruleData.maxHp;

        playerID = _playerID;
    }

    public GameObject character;
    public GameObject myPillow;
    public Transform myBodyTransform;
    public Transform myPillowTransform;
    public Transform myCameraTransform;
    public Rigidbody myBodyRigidbody;
    public Rigidbody myPillowRigidbody;
    public Camera myCamera;

    public BedJudgement bedJudgement;

    public int HP { get; private set; }
    public float remainthrowCT = 0;

    public bool canJump = true;
    public bool isDeath = false;
    public bool isHavePillow = true;
    public bool isInBedRange = false;
    public bool isInBed = false;
    public bool isDash = false;

    public Vector3 inBedPos = Vector3.zero;

    public int playerID;

    public void Damage()
    {
        if (isDeath || isInBed) return;
        HP--;
        if (HP <= 0)
        {
            GameManager.Instance.remainCharacters--;
            GameManager.Instance.resultIDs.Add(playerID);
            isDeath = true;
        }
    }
}
