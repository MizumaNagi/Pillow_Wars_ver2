using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    public CharacterData(GameObject _myObject, int _characterID, bool _isNpc)
    {
        isNpc = _isNpc;
        character = _myObject;
        Transform t = character.transform;
        myBodyTransform = t;
        pillow = t.GetChild(2).gameObject;
        myPillowTransform = pillow.transform;
        myPillowRigidbody = pillow.GetComponent<Rigidbody>();
        pillowCollider = pillow.GetComponent<CapsuleCollider>();
        myBodyRigidbody = character.GetComponent<Rigidbody>();
        bodyCollider = character.GetComponent<BoxCollider>();
        HP = GameManager.Instance.ruleData.maxHp;
        meshObjParent = t.GetChild(0).gameObject;

        if (_isNpc == false)
        {
            myCameraTransform = t.GetChild(1).transform;
            myCamera = t.GetChild(1).GetComponent<Camera>();

            GameObject myLoserCameraObj = GameObject.FindGameObjectWithTag("LoserCamera");
            myLoserCameraObj.SetActive(true);
            myLoserCamera = myLoserCameraObj.transform.GetChild(_characterID).GetComponent<Camera>();
            myLoserCamera.enabled = false;
        }

        characterID = _characterID;
    }

    public GameObject character;
    public GameObject pillow;
    public GameObject meshObjParent;
    //public GameObject myPillow;
    public Transform myBodyTransform;
    public Transform myPillowTransform;
    public Transform myCameraTransform;
    public Rigidbody myBodyRigidbody;
    public Rigidbody myPillowRigidbody;
    public Camera myCamera;
    public Camera myLoserCamera;
    public BoxCollider bodyCollider;
    public CapsuleCollider pillowCollider;

    public BedStatus bedStatus;
    public DoorAnimation doorAnimation;

    public int HP { get; private set; }
    public int hitPillowCount;
    public float remainthrowCT = 0;
    public float remainStunTime = 0;

    public bool isNpc = false;
    public bool canJump = true;
    public bool isDeath = false;
    public bool isHavePillow = true;
    public bool isInBedRange = false;
    public bool isInBed = false;
    public bool isDash = false;
    public bool isInDoor = false;
    public bool isSquat = false;

    public Vector3 inBedPos = Vector3.zero;

    private int characterID;

    public void Damage(bool isPieceDamage, bool isPercentDamage)
    {
        if (isDeath) { return; }
        if (isInBed && isPieceDamage == false) { return; }
        hitPillowCount++;
        if (hitPillowCount >= GameManager.Instance.ruleData.hitPillowCountOnDamage || isPercentDamage)
        {
            HP--;
            hitPillowCount = 0;
        }

        if (HP <= 0)
        {
            character.SetActive(false);
            Death();
        }
    }

    public void StunJudge()
    {
        int rnd = Random.Range(0, 100);
        if(GameManager.Instance.ruleData.stunPercent > rnd)
        {
            remainStunTime = GameManager.Instance.ruleData.pillowHeadShotStunTime;
        }
    }

    private void Death()
    {
        if (bedStatus != null)
        {
            bedStatus.ChangeEnableCollider(true);
            bedStatus = null;
        }

        if (isNpc == false)
        {
            myCamera.enabled = false;
            myLoserCamera.enabled = true;
        }

        isInBed = false;
        isDeath = true;

        GameManager.Instance.remainCharacters--;
        if (isNpc == false)
        {
            GameManager.Instance.remainPlayers--;
            GameManager.Instance.resultIDs.Add(characterID + 1);
        }
        //else GameManager.Instance.resultIDs.Add(characterID + 1);
    }

    public void HideCharacter(bool enable)
    {
        pillow.SetActive(!enable);
        meshObjParent.SetActive(!enable);
        bodyCollider.enabled = !enable;
        myBodyRigidbody.isKinematic = enable;
    }

    public int GetID(bool isNpc)
    {
        if (isNpc) return characterID - 100;
        else return characterID;
    }

    public int GetOriginalID()
    {
        return characterID;
    }

}
