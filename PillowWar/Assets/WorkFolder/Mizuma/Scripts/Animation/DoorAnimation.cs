using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = transform.parent.GetComponent<Animator>();
    }

    public void InteractDoor()
    {
        bool b = animator.GetBool("isOpen");
        animator.SetBool("isOpen", !b);
        animator.SetTrigger("trigger");
    }
}
