using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayerMover : CharacterMover
{
    // TODO:A.PlayerMover�͊֐�Only�ɂ����đ��̃X�N���v�g(PlayerInput����Ăяo��)B.������Update�֐��Ăяo���Ă����œ�����

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
