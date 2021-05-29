using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedJudgement : MonoBehaviour
{
    [SerializeField] private Collider myCollider;

    public void Start()
    {
        myCollider = GetComponent<BoxCollider>();
    }

    public void ChangeEnableCollider(bool b)
    {
        myCollider.enabled = b;
    }
}
