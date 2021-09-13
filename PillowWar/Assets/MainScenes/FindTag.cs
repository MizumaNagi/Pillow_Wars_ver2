using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1000)]
public class FindTag : MonoBehaviour
{
    public GameObject[] objs = new GameObject[100];

    public void Start()
    {
        objs = GameObject.FindGameObjectsWithTag("Player");
    }
}
