using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedStatus : MonoBehaviour
{
    private Collider myCollider;
    public float remainDamagetime;

    public int inCharacterID;
    public bool canIn = true;

    private void Start()
    {
        myCollider = GetComponent<BoxCollider>();
        ResetTime();
    }

    private void Update()
    {
        if (myCollider.enabled == true) return;
        remainDamagetime -= Time.deltaTime;
        if (remainDamagetime < 0) isTimeOver();
    }

    public void ChangeEnableCollider(bool isOut, int ID = -1)
    {
        inCharacterID = ID;

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
        if (inCharacterID < 100) PlayerManager.Instance.playerDatas[inCharacterID].Damage(true, false);
        else PlayerManager.Instance.npcDatas[inCharacterID - 100].Damage(true, false);
        ResetTime();
    }

    private void ResetTime()
    {
        remainDamagetime = GameManager.Instance.ruleData.inBedDamageTime;
    }
}
