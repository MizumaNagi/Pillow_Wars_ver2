using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    private Transform myTransform;

    public CharacterMover(Transform _transform)
    {
        myTransform = _transform;
    }

    public void Move(Vector2 _moveVec)
    {

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