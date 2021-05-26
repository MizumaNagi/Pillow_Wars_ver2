using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// キャラクター移動クラス
public class CharacterMover
{
    public void Move(Vector3 _movVec, CharacterData data)
    {
        Transform movTransform = data.myBodyTransform;

        Vector3 movVec = movTransform.rotation * _movVec * InputManager.Instance.moveData.moveSpd;
        movTransform.position += movVec * Time.deltaTime;
    }

    public void ViewMove(Vector3 _viewMovVec, CharacterData data)
    {
        Vector3 rotVec = _viewMovVec * InputManager.Instance.moveData.viewMoveSpd * Time.deltaTime;

        data.myBodyTransform.Rotate(0, rotVec.x, 0);
        data.myCameraTransform.Rotate(-rotVec.z, 0, 0);
    }

    public void Dash(CharacterData data, bool isDash)
    {
        data.isDash = isDash;
    }

    public void Jump(CharacterData data)
    {
        data.canJump = false;
        data.myBodyRigidbody.AddForce(0, InputManager.Instance.moveData.jumpForce/* * data.myBodyTransform.forward.y*/, 0);
    }

    public void PillowThrow(CharacterData data)
    {
        data.isHavePillow = false;
        Vector3 v = new Vector3(0.4f,1.5f,1.6f);
        data.myPillowTransform.localPosition = v;
        data.myPillowTransform.SetParent(PlayerManager.Instance.PillowParent);
        data.remainthrowCT = GameManager.Instance.ruleData.pillowThrowCT;
        data.myPillowRigidbody.isKinematic = false;
        data.myPillowRigidbody.AddForce(data.myCameraTransform.forward * InputManager.Instance.moveData.throwForce);
    }

    public void ToNonADS(CharacterData data)
    {
        if (data.myCamera.fieldOfView >= InputManager.Instance.moveData.maxFOV) { return; }

        float frameChgValueADS = InputManager.Instance.moveData.fovChangeSpd * Time.deltaTime;
        if (data.myCamera.fieldOfView + frameChgValueADS > InputManager.Instance.moveData.maxFOV)
        {
            data.myCamera.fieldOfView = InputManager.Instance.moveData.maxFOV;
        }
        else
        {
            data.myCamera.fieldOfView += frameChgValueADS;
        }
    }

    public void ToADS(CharacterData data)
    {
        if (data.myCamera.fieldOfView <= InputManager.Instance.moveData.minFOV) { return; }

        float frameChgValueADS = InputManager.Instance.moveData.fovChangeSpd * Time.deltaTime;
        if (data.myCamera.fieldOfView - frameChgValueADS < InputManager.Instance.moveData.minFOV)
        {
            data.myCamera.fieldOfView = InputManager.Instance.moveData.minFOV;
        }
        else
        {
            data.myCamera.fieldOfView -= frameChgValueADS;
        }
    }

    public void InteractBed(CharacterData data, bool isInBed, Vector3 bedPos)
    {
        data.isInBed = isInBed;
        if (isInBed == true) data.myBodyTransform.localPosition = bedPos;
    }
}