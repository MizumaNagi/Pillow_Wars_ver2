using UnityEngine;
using System.Text;

public class PillowController : MonoBehaviour
{
    public CharacterData characterData;

    [SerializeField] private PillowEffectPlay pillowEffectPlay;

    private int objNum;
    private float returnLimitPosY = -2f;
    private Vector3 defaultScale;

    public void Start()
    {
        StringBuilder sb = new StringBuilder(gameObject.name);
        objNum = int.Parse(sb.ToString());
        sb.Clear();
        defaultScale = transform.localScale;
    }

    private void Update()
    {
        if(GameManager.Instance.isPause == false)
        {
            float multi = InputManager.Instance.moveData.updateVelocityCoeffcient;
            characterData.myPillowRigidbody.velocity = Vector3.Scale(characterData.myPillowRigidbody.velocity, new Vector3(multi, 1, multi));
        }

        if (transform.localPosition.y <= returnLimitPosY)
        {
            ReturnPillow();
        }
    }

    public void OnCollisionEnter(Collision collison)
    {
        if (collison.gameObject.tag == "Ground")
        {
            pillowEffectPlay.MakeEffect(transform.position);
            ReturnPillow();
        }
        else if (collison.gameObject.tag == "Player")
        {
            StringBuilder sb = new StringBuilder(collison.gameObject.name);
            sb.Replace("Player","");
            sb.Replace("Npc", "");
            int playerNum = int.Parse(sb.ToString());
            sb.Clear();

            if (playerNum != objNum)
            {
                if(characterData.buffInfo.remainDoubleDmgCount > 0)
                {
                    if (playerNum >= 100) PlayerManager.Instance.npcDatas[playerNum - 100].Damage(false, false);
                    else PlayerManager.Instance.playerDatas[playerNum].Damage(false, false);
                }
                pillowEffectPlay.MakeEffect(transform.position);
                ReturnPillow();
            }
        }
    }
    
    private void ReturnPillow()
    {
        characterData.isHavePillow = true;
        characterData.pillowCollider.enabled = false;

        transform.SetParent(characterData.initAccessorieParentProperty.PillowParent);
        transform.localPosition = characterData.initAccessorieParentProperty.InitPillowPos;
        transform.localRotation = characterData.initAccessorieParentProperty.InitPillowRot;

        characterData.myPillowRigidbody.isKinematic = true;
        characterData.myPillowRigidbody.velocity = Vector3.zero;

        transform.localScale = defaultScale;
    }
}
