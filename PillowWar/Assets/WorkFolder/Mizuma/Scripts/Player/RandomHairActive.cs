using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHairActive : MonoBehaviour
{
    void Start()
    {
        int rndIndex = Random.Range(0, 2);
        if (rndIndex == 2) return;
        transform.GetChild(rndIndex).gameObject.SetActive(true);
    }
}
