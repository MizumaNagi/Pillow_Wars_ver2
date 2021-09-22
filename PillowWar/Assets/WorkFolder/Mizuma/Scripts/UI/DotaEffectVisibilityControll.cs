using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotaEffectVisibilityControll : MonoBehaviour
{
    [SerializeField] private GameObject effectParent;

    public void ChgVisibilityEffect(bool isActive)
    {
        effectParent.SetActive(isActive);
    }
}
