using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    Rigidbody playerRigidbody;

    float moveX, moveZ;
    bool moveZpermission, moveXpermission;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        if (moveZ != 0)
        {
            moveZpermission = true;
        }
        if (moveX != 0)
        {
            moveXpermission = true;
        }
    }

    void FixedUpdate()
    {
        if (moveZpermission || moveXpermission)
        {
            moveZpermission = false;
            moveXpermission = false;
            playerRigidbody.velocity = transform.rotation * new Vector3(moveX * moveSpeed, 0, moveZ * moveSpeed) * Time.deltaTime * 100;
        }
        else
        {
            playerRigidbody.velocity = Vector3.zero;
        }
    }
}
