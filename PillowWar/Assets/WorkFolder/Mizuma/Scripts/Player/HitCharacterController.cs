using UnityEngine;
using System.Text;

public class HitCharacterController : MonoBehaviour
{
    [SerializeField] private int objNum;
    public bool isNpc;

    private void Start()
    {
        //StringBuilder sb = new StringBuilder(gameObject.name);
        //sb.Replace("Player","");
        //sb.Replace("Npc", "");
        //objNum = int.Parse(sb.ToString());
        //sb.Clear();
    }

    public void OnCollisionEnter(Collision collison)
    {
        if (collison.gameObject.tag == "Pillow")
        {
            int pillowNum = int.Parse(collison.gameObject.name);
            if (pillowNum == objNum) return;

            if (isNpc == true) PlayerManager.Instance.npcDatas[objNum - 100].Damage(false);
            else PlayerManager.Instance.playerDatas[objNum].Damage(false);
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
                PlayerManager.Instance.npcDatas[objNum - 100].isInBedRange = true;
                PlayerManager.Instance.npcDatas[objNum - 100].inBedPos = other.transform.position;
                BedStatus bed = other.GetComponent<BedStatus>();
                PlayerManager.Instance.npcDatas[objNum - 100].bedStatus = bed;
            }
            else
            {
                PlayerManager.Instance.playerDatas[objNum].isInBedRange = true;
                PlayerManager.Instance.playerDatas[objNum].inBedPos = other.transform.position;
                BedStatus bed = other.GetComponent<BedStatus>();
                PlayerManager.Instance.playerDatas[objNum].bedStatus = bed;
            }
        }

        if (other.gameObject.tag == "Door")
        {
            if (isNpc == true)
            {
                PlayerManager.Instance.npcDatas[objNum - 100].isInDoor = true;
                PlayerManager.Instance.npcDatas[objNum - 100].doorAnimation = other.GetComponent<DoorAnimation>();
            }
            else
            {
                PlayerManager.Instance.playerDatas[objNum].isInDoor = true;
                PlayerManager.Instance.playerDatas[objNum].doorAnimation = other.GetComponent<DoorAnimation>();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Bed")
        {
            if (isNpc == true)
            {
                PlayerManager.Instance.npcDatas[objNum - 100].isInBedRange = false;
                PlayerManager.Instance.npcDatas[objNum - 100].bedStatus = null;
            }
            else
            {
                PlayerManager.Instance.playerDatas[objNum].isInBedRange = false;
                PlayerManager.Instance.playerDatas[objNum].bedStatus = null;
            }
        }

        if(other.gameObject.tag == "Door")
        {
            if (isNpc == true)
            {
                PlayerManager.Instance.npcDatas[objNum - 100].isInDoor = false;
                PlayerManager.Instance.npcDatas[objNum - 100].doorAnimation = null;
            }
            else
            {
                PlayerManager.Instance.playerDatas[objNum].isInDoor = false;
                PlayerManager.Instance.playerDatas[objNum].doorAnimation = null;
            }
            
        }
    }
}
