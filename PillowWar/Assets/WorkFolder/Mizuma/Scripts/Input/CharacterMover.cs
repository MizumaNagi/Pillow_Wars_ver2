using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// キャラクター移動クラス
public class CharacterMover
{
    public void Move(Vector3 _movVec, CharacterData data)
    {
        Transform movTransform = data.myBodyTransform;
        Rigidbody movRb = data.myBodyRigidbody;

        Vector3 movVec = movTransform.rotation * _movVec * InputManager.Instance.moveData.moveForce;

        //Vector3 movVec;
        //if(data.isSquat) movVec = movTransform.rotation * _movVec * InputManager.Instance.moveData.squatMoveSpdLimit;
        //else movVec = movTransform.rotation * _movVec * InputManager.Instance.moveData.moveForce;

        //movTransform.position += movVec * Time.deltaTime;
        movRb.AddForce(movVec * Time.deltaTime);

        if(data.isSquat)
        {
            if (movRb.velocity.magnitude > InputManager.Instance.moveData.squatMoveSpdLimit)
            {
                movRb.velocity /= 1.1f;
            }
        }
        else
        {
            if (movRb.velocity.magnitude > InputManager.Instance.moveData.walkMoveSpdLimit)
            {
                movRb.velocity /= 1.1f;
            }
        }
        
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
        data.myBodyRigidbody.AddForce(0, InputManager.Instance.moveData.jumpForce, 0);
    }

    public void PillowThrow(CharacterData data, bool isNpc)
    {
        float angle = 15f;
        float missVec = 0.3f;

        data.isHavePillow = false;
        data.pillowCollider.enabled = true;

        data.myPillowTransform.localPosition = new Vector3(0,0,1);
        data.myPillowRigidbody.angularVelocity = Vector3.forward;

        data.myPillowTransform.SetParent(PlayerManager.Instance.PillowParent);
        data.remainthrowCT = GameManager.Instance.ruleData.pillowThrowCT;
        data.myPillowRigidbody.isKinematic = false;

        Quaternion forwardRotation = data.myBodyTransform.rotation;
        Vector3 angleVec = new Vector3(0, Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad)).normalized;
        Vector3 rndVec = new Vector3(Random.Range(-missVec, missVec), Random.Range(-missVec, missVec), Random.Range(-missVec, missVec));

        data.myPillowRigidbody.AddForce(forwardRotation * (angleVec + rndVec) * InputManager.Instance.moveData.throwForce, ForceMode.Acceleration);
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
        if (data.bedStatus == null)
        {
            Debug.LogWarning("data.bedStatus == null \nオブジェクトが破棄されているか確認");
        }

        data.isInBed = isInBed;
        if (isInBed == true)
        {
            data.bedStatus.ChangeEnableCollider(false, data);
            data.myBodyTransform.localPosition = bedPos;
        }
        else
        {
            data.bedStatus.ChangeEnableCollider(true, null);
            data.bedStatus = null;
        }
        data.HideCharacter(isInBed);
    }

    public void Squat(CharacterData data, bool isSquated)
    {
        if (isSquated)
        {
            data.myCameraTransform.localPosition = InputManager.Instance.moveData.standingCameraPos;
            Debug.Log("しゃがみ解除");
        }
        else
        {
            data.myCameraTransform.localPosition = InputManager.Instance.moveData.squatingCameraPos;
            Debug.Log("しゃがみ開始");
        }

        data.isSquat = !isSquated;
    }

    // 参考:
    private Vector3 CalculateVelocity(Vector3 pointA, Vector3 pointB, float angle)
    {
        // 射出角をラジアンに変換
        float rad = angle * Mathf.PI / 180;

        // 水平方向の距離x
        float x = Vector2.Distance(new Vector2(pointA.x, pointA.z), new Vector2(pointB.x, pointB.z));

        // 垂直方向の距離y
        float y = pointA.y - pointB.y;

        // 斜方投射の公式を初速度について解く
        float speed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(x, 2) / (2 * Mathf.Pow(Mathf.Cos(rad), 2) * (x * Mathf.Tan(rad) + y)));

        if (float.IsNaN(speed))
        {
            // 条件を満たす初速を算出できなければVector3.zeroを返す
            return Vector3.zero;
        }
        else
        {
            return (new Vector3(pointB.x - pointA.x, x * Mathf.Tan(rad), pointB.z - pointA.z).normalized * speed);
        }
    }
}