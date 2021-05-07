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
        playerInput = GetComponent<PlayerInput>();
        myCamera = GetComponentInChildren<Camera>();
        //characterController = GetComponent<CharacterController>();
    }

    public void Update()
    {
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
            Debug.Log(rotVec);

            transform.Rotate(0, rotVec.x, 0);
            myCamera.transform.Rotate(-rotVec.z, 0, 0);
        }
        else
        {
            viewInput = Vector3.zero;
        }

        float rotX = myCamera.transform.rotation.x;
        if(rotX < -15f || rotX > 15f)
        {
            if (rotX < -15f) rotX = -15f;
            else rotX = 15f;
            myCamera.transform.rotation = Quaternion.Euler(new Vector3(rotX, transform.rotation.y, transform.rotation.z));
        }

    }

    public void OnMove(InputValue value)
    {
        Debug.Log("Move");
        moveInput = new Vector3(value.Get<Vector2>().x, 0f, value.Get<Vector2>().y);
    }

    public void OnViewMove(InputValue value)
    {
        Debug.Log("ViewMove");
        viewInput = new Vector3(value.Get<Vector2>().x, 0f, value.Get<Vector2>().y);
    }
}
