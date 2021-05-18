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

    public void Jump(CharacterData data)
    {
        data.canJump = false;
        data.myBodyRigidbody.AddForce(0, InputManager.Instance.moveData.jumpForce/* * data.myBodyTransform.forward.y*/, 0);
    }

    public void PillowThrow(CharacterData data)
    {
        data.isHavePillow = false;
        data.myPillowTransform.SetParent(PlayerManager.Instance.pillowParent);
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
}

// input system を使用した動き
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMover : MonoBehaviour
{
    //public CharacterController characterController;
    private PlayerInput playerInput;
    public Camera myCamera;

    private Vector3 moveInput;
    private Vector3 viewInput;

    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float viewSpeed = 2f;

    public void Start()
    {
        //playerInput = GetComponent<PlayerInput>();
        myCamera = GetComponentInChildren<Camera>();
    }

    public void Update()
    {
        moveInput = InputManager.Instance.inputStatuses[0].leftStickVec;
        viewInput = InputManager.Instance.inputStatuses[0].rightStickVec;

        if (moveInput.magnitude > 0.2f)
        {
            Vector3 movVec = gameObject.transform.rotation * moveInput * walkSpeed;
            transform.position += movVec * Time.deltaTime;
        }
        else
        {
            moveInput = Vector3.zero;
        }

        if (viewInput.magnitude > 0.1f)
        {
            Vector3 rotVec = viewInput * viewSpeed * Time.deltaTime;

            transform.Rotate(0, rotVec.x, 0);
            myCamera.transform.Rotate(-rotVec.z, 0, 0);
        }
        else
        {
            viewInput = Vector3.zero;
        }

        float rotX = myCamera.transform.localRotation.x;
        if(rotX > 15f)
        {
            myCamera.transform.localRotation = Quaternion.Euler(rotX, transform.localRotation.y, transform.localRotation.z);
        }

        //if(rotX < -15f || rotX > 15f)
        //{
        //    if (rotX < -15f) rotX = -15f;
        //    else rotX = 15f;
        //    myCamera.transform.rotation = Quaternion.Euler(new Vector3(rotX, transform.rotation.y, transform.rotation.z));
        //}
    }
}
*/