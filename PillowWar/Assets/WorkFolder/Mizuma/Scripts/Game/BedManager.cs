using System;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class BedManager : SingletonMonoBehaviour<BedManager>
{
    [SerializeField] private int activeBeds = 5;

    private List<int> beforeArr = new List<int>();
    private List<int> afterArr = new List<int>();

    public List<BoxCollider> bedColliders = new List<BoxCollider>();

    public void Init()
    {
        RandomObjActive();
        AllBedChgActive(true);
    }

    public void EndReset()
    {
        AllBedChgActive(false);
    }

    private void RandomObjActive()
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
            bedColliders.Add(transform.GetChild(afterArr[i]).GetComponentInChildren<BoxCollider>());
            transform.GetChild(afterArr[i]).GetComponentInChildren<BedStatus>().InitSet(true);
            beforeArr.RemoveAt(activeNum);
            transform.GetChild(afterArr[i]).gameObject.SetActive(true);
        }

        foreach(int disableNum in beforeArr.ToArray())
        {
            transform.GetChild(disableNum).GetComponentInChildren<BedStatus>().InitSet(false);
        }
    }

    private void AllBedChgActive(bool isActive)
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(isActive);
        }

        ResetStatus();
    }

    public void ResetStatus()
    {
        beforeArr.Clear();
        afterArr.Clear();
        bedColliders.Clear();
    }
}
