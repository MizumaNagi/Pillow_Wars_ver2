using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedStatus : MonoBehaviour
{
    private Collider myCollider;
    public float remainDamagetime;

    public CharacterData data;
    public bool canIn = true;

    private void Start()
    {
        myCollider = GetComponent<BoxCollider>();
        ResetTime();
    }

    private void Update()
    {
        if (myCollider.enabled == true) return;
        if (GameEventScript.Instance.canAction == true) remainDamagetime -= Time.deltaTime;
        if (remainDamagetime < 0) isTimeOver();
    }

    public void ChangeEnableCollider(bool isOut, CharacterData data)
    {
        myCollider.enabled = isOut;
        canIn = isOut;

        if (myCollider == null) Debug.Log(gameObject.name);

        if (isOut == true)
        {
            ResetTime();
        }
    }

    private void isTimeOver()
    {
        ResetTime();

        //CharacterData data;
        //if (inCharacterID < 100) data = PlayerManager.Instance.playerDatas[inCharacterID];
        //else data = PlayerManager.Instance.npcDatas[inCharacterID - 100];

        if (data.isInBed == true) return;
        data.Damage(true, false);
    }

    private void ResetTime()
    {
        remainDamagetime = GameManager.Instance.ruleData.inBedDamageTime;
    }
}
