using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    public CharacterData(GameObject _myObject, int _characterID, bool _isNpc)
    {
        character = _myObject;
        characterID = _characterID;
        isNpc = _isNpc;

        initAccessorieParentProperty = character.GetComponentInChildren<InitAccessorieParentProperty>();
        buffInfo = character.GetComponentInChildren<BuffInfo>();

        Transform t = character.transform;
        myBodyTransform = t;
        if (_isNpc == false)
        {
            meshObjParent = t.GetChild(1).gameObject;
        }
        else
        {
            meshObjParent = t.GetChild(2).gameObject;
        }
        
        myCameraTransform = t.GetChild(0).transform;
        pillow = initAccessorieParentProperty.PillowParent.GetChild(0).gameObject;
        myPillowTransform = pillow.transform;
        myPillowRigidbody = pillow.GetComponent<Rigidbody>();
        pillowCollider = pillow.GetComponent<BoxCollider>();
        myBodyRigidbody = character.GetComponent<Rigidbody>();
        bodyCollider = character.GetComponent<BoxCollider>();
        HP = GameManager.Instance.ruleData.maxHp;

        if (_isNpc == false)
        {
            myCamera = myCameraTransform.GetComponent<Camera>();
            cameraController = myCameraTransform.GetComponent<CameraController>();
            cameraController.Init(characterID);

            GameObject myLoserCameraObj = GameObject.FindGameObjectWithTag("LoserCamera");
            myLoserCameraObj.SetActive(true);
            myLoserCamera = myLoserCameraObj.transform.GetChild(_characterID).GetComponent<Camera>();
            myLoserCamera.enabled = false;
        }
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
    public BoxCollider pillowCollider;

    public BedStatus bedStatus;
    public DoorAnimation doorAnimation;
    public CameraController cameraController;
    public InitAccessorieParentProperty initAccessorieParentProperty;
    public AnimatorManager animatorManager;
    public BuffInfo buffInfo;

    //public int HP { get; private set; }
    public int HP;
    public int hitPillowCount;
    public float remainthrowCT = 0;
    public float remainStunTime = 0;
    public float remainCanBedInTime = 0;

    public bool isNpc = false;
    public bool canJump = true;
    public bool isDeath = false;
    public bool isHavePillow = true;
    public bool isInBedRange = false;
    public bool isInBed = false;
    public bool isDash = false;
    public bool isSquat = false;
    public bool isZoom = false;

    public Vector3 inBedPos = Vector3.zero;

    private int characterID;

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="isPieceDamage">貫通攻撃か</param>
    /// <param name="isPercentDamage">割合ダメージか</param>
    public void Damage(bool isPieceDamage, bool isPercentDamage)
    {
        if (isDeath) { return; }
        if (isInBed && isPieceDamage == false) { return; }
        hitPillowCount++;
        if ((hitPillowCount >= GameManager.Instance.ruleData.hitPillowCountOnDamage) || isPieceDamage || isPercentDamage)
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

    /// <summary>
    /// スタン判定
    /// </summary>
    public void StunJudge()
    {
        int rnd = Random.Range(0, 100);
        if(GameManager.Instance.ruleData.stunPercent > rnd)
        {
            remainStunTime = GameManager.Instance.ruleData.pillowHeadShotStunTime;
        }
    }

    /// <summary>
    /// 死亡処理
    /// </summary>
    private void Death()
    {
        if (bedStatus != null)
        {
            bedStatus.ChangeBedActive(true, this);
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
    }

    /// <summary>
    /// キャラクターの有効/無効化
    /// </summary>
    /// <param name="enable">プレイヤーの状態</param>
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
