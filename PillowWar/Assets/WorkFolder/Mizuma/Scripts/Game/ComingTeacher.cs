using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComingTeacher : MonoBehaviour
{
    public Vector3 cameraPos;
    public Vector3 cameraRot;

    [SerializeField] private Transform cameraTrans1;
    [SerializeField] private Transform cameraTrans2;
    [SerializeField] private Transform cameraTrans3;
    [SerializeField] private Transform cameraTrans4;
    [SerializeField] private SpotDotaEffectVisibility spotDotaEffectVisibility;

    [SerializeField] private Light[] spotALights;
    [SerializeField] private Light[] spotBLights;

    private bool isAnimationing;
    private int haveCameraCount;

    private void Start()
    {
        haveCameraCount = GameManager.Instance.joinPlayers;

        cameraTrans1 = PlayerManager.Instance.playerDatas[0].myCameraTransform;
        cameraTrans2 = PlayerManager.Instance.playerDatas[1].myCameraTransform;
        if (haveCameraCount == 2) return;
        cameraTrans3 = PlayerManager.Instance.playerDatas[2].myCameraTransform;
        cameraTrans4 = PlayerManager.Instance.playerDatas[3].myCameraTransform;
    }

    private void Update()
    {
        if(isAnimationing)
        {
            cameraTrans1.position = cameraPos;
            cameraTrans2.position = cameraPos;
            if (haveCameraCount == 2) return;
            cameraTrans3.position = cameraPos;
            cameraTrans4.position = cameraPos;
        }
    }

    public void VisibilityLightsSpotA(int isActive)
    {
        if(isActive == 0)
        {
            foreach(Light light in spotALights)
            {
                light.enabled = true;
            }
        }
        else
        {
            foreach (Light light in spotALights)
            {
                light.enabled = false;
            }
        }
    }

    public void VisibilityLightsSpotB(int isActive)
    {
        if (isActive == 0)
        {
            foreach (Light light in spotBLights)
            {
                light.enabled = true;
            }
        }
        else
        {
            foreach (Light light in spotBLights)
            {
                light.enabled = false;
            }
        }
    }

    private void StartAnimation()
    {
        for(int i = 0; i < 2; i++)
        {
            spotDotaEffectVisibility.ChgVisibilityEffect(false, i);
        }
        isAnimationing = true;
    }

    private void EndAnimation()
    {
        isAnimationing = false;
        GameEventScript.Instance.canAction = true;

        foreach(CharacterData data in PlayerManager.Instance.playerDatas.ToArray())
        {
            if (data.isInBed) continue;
            data.myCameraTransform.localPosition = InputManager.Instance.moveData.standingCameraPos;
            data.myCameraTransform.localRotation = Quaternion.identity;
        }
    }
}
