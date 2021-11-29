using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedStatus : MonoBehaviour
{
    [SerializeField] private GameObject emptyBed;
    [SerializeField] private GameObject fullBed;
    [SerializeField] public BoxCollider myEventCollider;

    private bool canInteract;

    public float remainDamageTime;
    public CharacterData data;
    public bool canIn = true;

    private void Start()
    {
        ResetTime();
    }

    private void Update()
    {
        // �x�b�h���g�p��Ԃȏ�ꍇ�������Ȃ�
        if (emptyBed.activeSelf == true || canInteract == false) return;

        // �ړ��\����|�[�Y��ԂŐ����_���[�W�̃��L���X�g����
        if (GameEventScript.Instance.canAction == true && GameManager.Instance.isPause == false) remainDamageTime -= Time.deltaTime;
        if (remainDamageTime < 0) isTimeOver();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isOut">�z�c����o���Ԃ�</param>
    /// <param name="data">�z�c�Ɋ�����v���C���[�f�[�^</param>
    public void ChangeBedActive(bool isOut, CharacterData data)
    {
        emptyBed.SetActive(isOut);
        fullBed.SetActive(!isOut);

        this.data = data;

        canIn = isOut;

        if (isOut == true)
        {
            ResetTime();
        }
    }

    /// <summary>
    /// ���Ԍo�߂ɂ�鐇���_���[�W
    /// </summary>
    private void isTimeOver()
    {
        ResetTime();

        if (data.isInBed == false) return;
        data.Damage(true, false);
    }

    private void ResetTime()
    {
        remainDamageTime = GameManager.Instance.ruleData.inBedDamageTime;
    }

    /// <summary>
    /// �e�X�e�[�^�X�̃��Z�b�g
    /// </summary>
    /// <param name="canInteract"></param>
    public void InitSet(bool canInteract)
    {
        this.canInteract = canInteract;
        emptyBed.SetActive(canInteract);
        fullBed.SetActive(!canInteract);
        myEventCollider.enabled = canInteract;
    }
}
