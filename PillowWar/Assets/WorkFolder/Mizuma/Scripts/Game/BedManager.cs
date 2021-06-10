using System;
using UnityEngine;
using System.Collections.Generic;

public class BedManager : MonoBehaviour
{
    [SerializeField] private int activeBeds = 5;

    public List<int> beforeArr = new List<int>();
    public List<int> afterArr = new List<int>();

    private void Start()
    {
        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            beforeArr.Add(i);
        }

        for (int i = 0; i < activeBeds; i++)
        {
            int activeNum = UnityEngine.Random.Range(0, beforeArr.Count);
            afterArr.Add(beforeArr[activeNum]);
            beforeArr.RemoveAt(activeNum);
            transform.GetChild(afterArr[i]).gameObject.SetActive(true);
            transform.GetChild(afterArr[i]).localEulerAngles = new Vector3(0, UnityEngine.Random.Range(0, 180), 0);
        }
    }
}
