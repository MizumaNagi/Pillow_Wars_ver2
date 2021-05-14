using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowController : MonoBehaviour
{
    public CharacterData characterData;

    public void UpdateMethod()
    {

    }

    public void OnCollisionEnter(Collision collison)
    {
        if (collison.gameObject.tag == "Ground")
        {
            transform.SetParent(characterData.character.transform);
            transform.localPosition = InputManager.Instance.moveData.pillowSpawnPos;
            characterData.myPillowRigidbody.isKinematic = true;
            characterData.myPillowRigidbody.velocity = Vector3.zero;
        }
    }  
}
