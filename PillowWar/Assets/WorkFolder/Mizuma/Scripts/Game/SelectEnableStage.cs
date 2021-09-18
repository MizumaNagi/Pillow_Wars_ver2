using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectEnableStage : MonoBehaviour
{
    [SerializeField] private Transform stageParent;

    void Start()
    {
        for(int i = 0; i < stageParent.childCount; i++)
        {
            stageParent.GetChild(i).gameObject.SetActive(false);
        }

        stageParent.GetChild(GameManager.Instance.selectStageNo).gameObject.SetActive(true);
    }
}
