using UnityEngine;
using System.Text;

public class PillowController : MonoBehaviour
{
    public CharacterData characterData;
    private int objNum;

    public void Start()
    {
        StringBuilder sb = new StringBuilder(gameObject.name);
        objNum = int.Parse(sb.ToString());
        sb.Clear();
    }

    public void UpdateMethod()
    {

    }

    public void OnCollisionEnter(Collision collison)
    {
        if (collison.gameObject.tag == "Ground")
        {
            ReturnPillow();
        }
        else if (collison.gameObject.tag == "Player")
        {
            StringBuilder sb = new StringBuilder(collison.gameObject.name);
            sb.Replace("Player","");
            sb.Replace("Npc", "");
            int playerNum = int.Parse(sb.ToString());
            sb.Clear();

            if (playerNum != objNum) ReturnPillow();
        }
    }
    
    private void ReturnPillow()
    {
        characterData.isHavePillow = true;
        transform.SetParent(characterData.character.transform);
        transform.localPosition = InputManager.Instance.moveData.pillowSpawnPos;
        characterData.myPillowRigidbody.isKinematic = true;
        characterData.myPillowRigidbody.velocity = Vector3.zero;
    }
}
