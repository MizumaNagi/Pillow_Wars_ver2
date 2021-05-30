using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedStatus : MonoBehaviour
{
    public Collider myCollider;
    public float remainDamagetime;

    public CharacterData cd;

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

    public void ChangeEnableCollider(bool isOut)
    {
        myCollider.enabled = isOut;

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
            cd.Damage(true);
            ResetTime();
        }
    }

    private void ResetTime()
    {
        remainDamagetime = GameManager.Instance.ruleData.inBedDamageTime;
    }
}
