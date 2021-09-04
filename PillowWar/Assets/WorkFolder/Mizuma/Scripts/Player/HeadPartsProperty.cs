using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadPartsProperty : MonoBehaviour
{
    [SerializeField] private CameraController cameraController;
    public GameObject[] headsParts;
    public bool isNpc;
    
    public void InitSetTag(int i)
    {
        foreach(GameObject obj in headsParts)
        {
            obj.layer = i + 23;
        }
    }

    public void EnableMyHeadParts()
    {
        if (isNpc) return;
        cameraController.VisibilityMyHeadParts(true);
    }
    public void DisableMyHeadParts()
    {
        if (isNpc) return;
        cameraController.VisibilityMyHeadParts(false);
    }
}
