using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectEnableStage : MonoBehaviour
{
    [SerializeField] private Transform stageParent;
    [SerializeField] private Transform firstStage;
    [SerializeField] private Transform secondStage;

    void Start()
    {
        if(GameManager.Instance.selectStageNo == 0)
        {
            firstStage.gameObject.SetActive(true);
            secondStage.gameObject.SetActive(false);
        }
        else
        {
            firstStage.gameObject.SetActive(false);
            secondStage.gameObject.SetActive(true);
        }
    }
}
