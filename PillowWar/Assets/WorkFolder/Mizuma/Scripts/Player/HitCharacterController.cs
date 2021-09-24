using UnityEngine;
using System.Text;

public class HitCharacterController : MonoBehaviour
{
    public int objNum;
    public bool isNpc;

    public void OnCollisionEnter(Collision collison)
    {
        if (collison.gameObject.tag == "Pillow")
        {
            int pillowNum = int.Parse(collison.gameObject.name);
            if (pillowNum == objNum) return;

            AudioManager.Instance.SEPlay(SEName.HitPillow);

            CharacterData damagedCharaData = null;
            if (isNpc == true) damagedCharaData = PlayerManager.Instance.npcDatas[objNum - 100];
            else damagedCharaData = PlayerManager.Instance.playerDatas[objNum];

            CharacterData throwCharaData = null;
            if (pillowNum >= 100) throwCharaData = PlayerManager.Instance.npcDatas[pillowNum - 100];
            else throwCharaData = PlayerManager.Instance.playerDatas[pillowNum];
            throwCharaData.buffInfo.remainDoubleDmgCount--;

            // ヘッドショット判定
            damagedCharaData.Damage(false, false);
            if (collison.transform.position.y > transform.position.y + GameManager.Instance.ruleData.headShotBorderLocalPosY
                && damagedCharaData.remainStunTime < -GameManager.Instance.ruleData.stunRegistTime)
            {
                damagedCharaData.StunJudge();
            }
        }

        if (collison.gameObject.tag == "Ground")
        {
            if (isNpc == true) PlayerManager.Instance.npcDatas[objNum - 100].canJump = true;
            else PlayerManager.Instance.playerDatas[objNum].canJump = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bed")
        {
            if (isNpc == true)
            {
                //Debug.Log("接触");
                //PlayerManager.Instance.npcDatas[objNum - 100].isInBedRange = true;
                //PlayerManager.Instance.npcDatas[objNum - 100].inBedPos = other.transform.position;
                //BedStatus bed = other.GetComponent<BedStatus>();
                //PlayerManager.Instance.npcDatas[objNum - 100].bedStatus = bed;
            }
            else
            {
                PlayerManager.Instance.playerDatas[objNum].isInBedRange = true;
                PlayerManager.Instance.playerDatas[objNum].inBedPos = other.transform.position;
                BedStatus bed = other.GetComponentInParent<BedStatus>();
                PlayerManager.Instance.playerDatas[objNum].bedStatus = bed;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Bed")
        {
            if (isNpc == true)
            {
                //PlayerManager.Instance.npcDatas[objNum - 100].isInBedRange = false;
                //PlayerManager.Instance.npcDatas[objNum - 100].bedStatus = null;
            }
            else
            {
                PlayerManager.Instance.playerDatas[objNum].isInBedRange = false;
                PlayerManager.Instance.playerDatas[objNum].bedStatus = null;
            }
        }
    }
}
