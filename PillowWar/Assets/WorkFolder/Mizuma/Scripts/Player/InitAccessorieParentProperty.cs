using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitAccessorieParentProperty : MonoBehaviour
{
    [SerializeField] private Transform hairParent;
    [SerializeField] private Transform pillowParent;

    public Vector3 InitHairPos { get; private set; }
    public Quaternion InitHairRot { get; private set; }
    public Vector3 InitPillowPos { get; private set; }
    public Quaternion InitPillowRot { get; private set; }
    public Transform HairParent { get { return hairParent; } }
    public Transform PillowParent { get { return pillowParent; } }

    private void Start()
    {
        InitHairPos = hairParent.GetChild(0).localPosition;
        InitHairRot = hairParent.GetChild(0).localRotation;

        InitPillowPos = pillowParent.GetChild(0).localPosition;
        InitPillowRot = pillowParent.GetChild(0).localRotation;
    }
}
