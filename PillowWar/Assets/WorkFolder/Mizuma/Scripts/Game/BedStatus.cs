using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedStatus : MonoBehaviour
{
    [SerializeField] private GameObject emptyBed;
    [SerializeField] private GameObject fullBed;

    private Collider myEventCollider;
    public float remainDamagetime;

    public CharacterData data;
    public bool canIn = true;

    private void Start()
    {
        emptyBed.SetActive(true);
        fullBed.SetActive(false);

        myEventCollider = GetComponent<BoxCollider>();
        ResetTime();
    }

    private void Update()
    {
        //if (myEventCollider.enabled == true) return;
        if (emptyBed.activeSelf == true) return;

        if (GameEventScript.Instance.canAction == true) remainDamagetime -= Time.deltaTime;
        if (remainDamagetime < 0) isTimeOver();
    }

    public void ChangeEnableCollider(bool isOut, CharacterData data)
    {
        //myEventCollider.enabled = isOut;
        emptyBed.SetActive(isOut);
        fullBed.SetActive(!isOut);

        this.data = data;

        canIn = isOut;

        //if (myEventCollider == null) Debug.Log(gameObject.name);

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

        if (data.isInBed == false) return;
        Debug.Log(data.HP);
        data.Damage(true, false);
    }

    private void ResetTime()
    {
        remainDamagetime = GameManager.Instance.ruleData.inBedDamageTime;
    }
}
