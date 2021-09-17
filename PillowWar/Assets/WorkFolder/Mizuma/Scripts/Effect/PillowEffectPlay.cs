using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowEffectPlay : MonoBehaviour
{
    [SerializeField] private ParticleSystem hitPillowHit;

    public void MakeEffect(Vector3 makePos)
    {
        Instantiate(hitPillowHit, makePos, Quaternion.identity);
    }
}
