using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadPartsProperty : MonoBehaviour
{
    [SerializeField] private CameraController cameraController;
    public GameObject[] headsParts;
    
    public void InitSetTag(int i)
    {
        foreach(GameObject obj in headsParts)
        {
            obj.layer = i + 23;
        }
    }

    public void EnableMyHeadParts()
    {
        cameraController.VisibilityMyHeadParts(true);
    }
    public void DisableMyHeadParts()
    {
        cameraController.VisibilityMyHeadParts(false);
    }
}
