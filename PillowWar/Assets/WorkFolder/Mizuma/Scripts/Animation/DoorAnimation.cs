using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    private Animator animator;
    private bool isOpened = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void InteractDoor()
    {
        if (isOpened) animator.SetTrigger("triggerClose");
        else animator.SetTrigger("triggerOpen");

        isOpened = !isOpened;
    }
}
