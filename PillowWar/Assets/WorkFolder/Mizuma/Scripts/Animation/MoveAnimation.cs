using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimation : MonoBehaviour
{
    //[SerializeField] AnimatorManager animatorManager;
    //
    //public void MoveSEPlay()
    //{
    //    Debug.Log("a");
    //
    //    if (animatorManager.moveState == MoveState.Running) RunSEPlay();
    //    else if (animatorManager.moveState == MoveState.Forward ||
    //        animatorManager.moveState == MoveState.Back ||
    //        animatorManager.moveState == MoveState.Left ||
    //        animatorManager.moveState == MoveState.Right) WalkSEPlay();
    //}

    public void WalkSEPlay()
    {
        AudioManager.Instance.SEPlay(SEName.Walk);
    }

    public void RunSEPlay()
    {
        AudioManager.Instance.SEPlay(SEName.Run);
    }
}
