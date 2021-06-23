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

    public void ChangeEnableCollider(bool isOut, CharacterData data = null)
    {
        if (data != null) SetCharacterData(data);

        myCollider.enabled = isOut;
        HideCharacter(isOut);

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

    private void HideCharacter(bool isOut)
    {
        if (cd == null)
        {
            Debug.LogError("CharacterData‚ª‚ ‚è‚Ü‚¹‚ñ");
            return;
        }

        //cd.character.transform.GetChild(0).gameObject.SetActive(isOut);
    }

    private void SetCharacterData(CharacterData data)
    {
        cd = data;
    }
}
