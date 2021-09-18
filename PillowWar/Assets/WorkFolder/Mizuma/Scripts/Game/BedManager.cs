using System;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class BedManager : SingletonMonoBehaviour<BedManager>
{
    [SerializeField] private int activeBeds = 5;
    [SerializeField] private Transform firstMapBeds;
    [SerializeField] private Transform secondMapBeds;

    private List<int> beforeArr = new List<int>();
    private List<int> afterArr = new List<int>();

    public List<BoxCollider> bedColliders = new List<BoxCollider>();

    public void Init()
    {
        if (GameManager.Instance.selectStageNo == 0)
        {
            RandomObjActive(firstMapBeds);
            AllBedChgActive(firstMapBeds, true);
            AllBedChgActive(secondMapBeds, false);
        }
        else
        {
            RandomObjActive(secondMapBeds);
            AllBedChgActive(firstMapBeds, false);
            AllBedChgActive(secondMapBeds, true);
        }
    }

    public void EndReset()
    {
        AllBedChgActive(firstMapBeds, false);
        AllBedChgActive(secondMapBeds, false);
        ResetStatus();
    }

    private void RandomObjActive(Transform beds)
    {
        int childCount = beds.childCount;

        for (int i = 0; i < childCount; i++)
        {
            beforeArr.Add(i);
        }

        for (int i = 0; i < activeBeds; i++)
        {
            int activeNum = UnityEngine.Random.Range(0, beforeArr.Count);
            afterArr.Add(beforeArr[activeNum]);

            BedStatus bedStatus = beds.GetChild(afterArr[i]).GetComponent<BedStatus>();

            bedColliders.Add(bedStatus.myEventCollider);
            bedStatus.InitSet(true);
            beforeArr.RemoveAt(activeNum);
            beds.GetChild(afterArr[i]).gameObject.SetActive(true);
        }

        foreach(int disableNum in beforeArr.ToArray())
        {
            beds.GetChild(disableNum).GetComponentInChildren<BedStatus>().InitSet(false);
        }
    }

    private void AllBedChgActive(Transform beds, bool isActive)
    {
        foreach(Transform bed in beds)
        {
            bed.gameObject.SetActive(isActive);
        }
    }

    private void ResetStatus()
    {
        beforeArr.Clear();
        afterArr.Clear();
        bedColliders.Clear();
    }
}
