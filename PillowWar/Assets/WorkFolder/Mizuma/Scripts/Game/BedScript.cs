using System;
using UnityEngine;
using System.Collections.Generic;

public class BedScript : MonoBehaviour
{
    private int activeBeds = 5;

    public List<GameObject> objs = new List<GameObject>();
    public List<int> beforeArr = new List<int>();
    public List<int> afterArr = new List<int>();

    private void Start()
    {
        int childCount = transform.childCount;

        int[] beforeArray = new int[childCount];
        int[] afterArray = new int[activeBeds];

        for (int i = 0; i < childCount; i++)
        {
            beforeArr.Add(i);
        }

        for (int i = childCount; i > 0; i--)
        {
            int rnd = UnityEngine.Random.Range(0, i);
            afterArr.Add(beforeArr[rnd]);
            beforeArr.Remove(rnd);
        }

        for(int i = 0; i < activeBeds; i++)
        {
            transform.GetChild(afterArr[i]).gameObject.SetActive(true);
        }
    }
}
