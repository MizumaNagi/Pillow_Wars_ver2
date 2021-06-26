using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedStatus : MonoBehaviour
{
    private Collider myCollider;
    public float remainDamagetime;

    public CharacterData cd;
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
        isTimeOver();
    }

    public void ChangeEnableCollider(bool isOut, CharacterData data = null)
    {
        if (data != null) SetCharacterData(data);

        myCollider.enabled = isOut;
        if (myCollider == null) Debug.Log(gameObject.name);
        canIn = isOut;

        if(isOut == true)
        {
            ResetTime();
            cd = null;
        }
    }

    public void isTimeOver()
    {
        if (remainDamagetime < 0)
        {
            cd.Damage(true, false);
            ResetTime();
        }
    }

    private void ResetTime()
    {
        remainDamagetime = GameManager.Instance.ruleData.inBedDamageTime;
    }

    private void SetCharacterData(CharacterData data)
    {
        cd = data;
    }
}
