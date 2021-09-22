using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotDotaEffectVisibility : MonoBehaviour
{
    [SerializeField] private GameObject[] spotDotaEffectParents;

    public void ChgVisibilityEffect(bool isActive, int index)
    {
        spotDotaEffectParents[index].SetActive(isActive);
    }
}
