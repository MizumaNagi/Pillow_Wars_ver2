using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashTest : MonoBehaviour
{
    [SerializeField] private int objNum;
    [SerializeField] private GameObject prefab;

    [SerializeField] private bool heavyMethod;
    public static bool isAddHeavyMethodClass;

    void Awake()
    {
        isAddHeavyMethodClass = heavyMethod;
    }

    void Start()
    {
        for(int i = 0; i < objNum; i++)
        {
            GameObject obj = Instantiate(prefab, new Vector3(0,0,i * 2), Quaternion.identity);
            if(isAddHeavyMethodClass) obj.AddComponent<MyHeavyMethod>();
        }
    }
}