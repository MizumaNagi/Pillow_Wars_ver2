using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashTest : MonoBehaviour
{
    [SerializeField] private int objNum;
    [SerializeField] private GameObject prefab;

    void Start()
    {
        for(int i = 0; i < objNum; i++)
        {
            GameObject obj = Instantiate(prefab);
            //obj.AddComponent<MyHeavyMethod>();
        }
    }
}