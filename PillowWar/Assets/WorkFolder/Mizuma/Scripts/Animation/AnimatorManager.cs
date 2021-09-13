using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum MoveState
{
    Standing,
    Forward,
    Left,
    Back,
    Right,
    Running,
    Throwing,
    Null = -1
}

public class AnimatorManager : MonoBehaviour
{
    private int objNum;
    private Animator animator;
    private CharacterData charaData;
    public MoveState moveState = MoveState.Standing;
    public Vector3 moveVec;
    public Vector3 latestFramePos;
    private float rotY;

    private void Start()
    {
        animator = GetComponent<Animator>();

        StringBuilder sb = new StringBuilder(transform.parent.name);
        sb.Replace("Player","");
        sb.Replace("Npc", "");
        objNum = int.Parse(sb.ToString());
        sb.Clear();

        if (objNum < 100) charaData = PlayerManager.Instance.playerDatas[objNum];
        else charaData = PlayerManager.Instance.npcDatas[objNum - 100];

        charaData.animatorManager = this;

        latestFramePos = transform.position;
    }

    private void Update()
    {
        rotY = transform.eulerAngles.y;
        moveVec = transform.position - latestFramePos;
        latestFramePos = transform.position;

        animator.SetInteger("MoveValue", (int)moveState);

        if (moveState == MoveState.Throwing)
        {
            animator.Play("Throwing");
            moveState = MoveState.Null;
        }
        else moveState = SetMoveState();
    }

    private MoveState SetMoveState()
    {
        if (moveState == MoveState.Throwing)
        {
            return moveState;
        }
        else
        {
            // 走ってるか
            if (charaData.isDash == true)
            {
                moveState = MoveState.Running;
            }
            else
            {
                Debug.Log(Mathf.Abs(moveVec.magnitude) + " / " + gameObject.transform.parent.name);
                // 殆ど止まってる
                if (Mathf.Abs(moveVec.magnitude) < 0.015f) moveState = MoveState.Standing;
                else
                {
                    // 左右か前後かで言うなら前後移動
                    if (Mathf.Abs(moveVec.z) >= Mathf.Abs(moveVec.x))
                    {
                        // 絶対座標から見て前に進んでる
                        if (moveVec.z >= 0)
                        {
                            if (rotY >= 45 && rotY < 135) moveState = MoveState.Left;
                            else if (rotY >= 135 && rotY < 225) moveState = MoveState.Back;
                            else if (rotY >= 225 && rotY < 315) moveState = MoveState.Right;
                            else moveState = MoveState.Forward;
                        }
                        else
                        {
                            if (rotY >= 45 && rotY < 135) moveState = MoveState.Right;
                            else if (rotY >= 135 && rotY < 225) moveState = MoveState.Forward;
                            else if (rotY >= 225 && rotY < 315) moveState = MoveState.Left;
                            else moveState = MoveState.Back;
                        }
                    }
                    else
                    {
                        // 絶対座標から見て右に進んでる
                        if (moveVec.x >= 0)
                        {
                            if (rotY >= 45 && rotY < 135) moveState = MoveState.Forward;
                            else if (rotY >= 135 && rotY < 225) moveState = MoveState.Left;
                            else if (rotY >= 225 && rotY < 315) moveState = MoveState.Back;
                            else moveState = MoveState.Right;
                        }
                        else
                        {
                            if (rotY >= 45 && rotY < 135) moveState = MoveState.Back;
                            else if (rotY >= 135 && rotY < 225) moveState = MoveState.Right;
                            else if (rotY >= 225 && rotY < 315) moveState = MoveState.Forward;
                            else moveState = MoveState.Left;
                        }
                    }
                }
            }
        }

        return moveState;
    }

    public void TriggerThrow()
    {
        moveState = MoveState.Throwing;
    }
}