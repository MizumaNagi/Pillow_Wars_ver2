using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayerMover : CharacterMover
{
    // TODO:A.PlayerMoverは関数Onlyにさせて他のスクリプト(PlayerInputから呼び出す)B.ここでUpdate関数呼び出してここで動かす

    [SerializeField] private float moveSpd;
    private Transform myTramsform;

    public PlayerMover(Transform _transform) : base(_transform)
    {

    }

    public void Start()
    {
        myTramsform = this.transform;
    }

    public void Update()
    {
        Vector3 inputVec = new Vector3(Input.GetAxis("Xbox_Axis_L_Horizontal_P1"), 0f, -Input.GetAxis("Xbox_Axis_L_Vertical_P1"));

        Move(inputVec);
    }

    public void Move(Vector3 vec)
    {
        myTramsform.position += vec * moveSpd * Time.deltaTime;
    }
}
