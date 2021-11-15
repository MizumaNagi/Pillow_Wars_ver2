using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    [SerializeField] private DotaEffectVisibilityControll dotaEffectVisibilityControll;

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

    public void DoorCloseFinish()
    {
        Debug.Log("Teacher Attacker Start 2");
        GameEventScript.Instance.canAction = true;
    }

    public void OpenDoorPlaySE()
    {
        dotaEffectVisibilityControll.ChgVisibilityEffect(false);
        AudioManager.Instance.SEPlay(SEName.OpenDoor);
    }

    public void CloseDoorPlaySE()
    {
        AudioManager.Instance.SEPlay(SEName.CloseDoor);
    }

    public void TeacherAppearsPlaySE()
    {
        AudioManager.Instance.SEPlay(SEName.TeacherAppears);
    }
}
