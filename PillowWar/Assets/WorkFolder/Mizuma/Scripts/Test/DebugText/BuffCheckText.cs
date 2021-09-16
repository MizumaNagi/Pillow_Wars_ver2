using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(9999)]
public class BuffCheckText : MonoBehaviour
{
    public Text text;
    private BuffInfo buffInfo;

    private void Start()
    {
        buffInfo = PlayerManager.Instance.playerDatas[1].buffInfo;
    }

    private void Update()
    {
        if (buffInfo.remainBigPillowTime > 0) text.text = "���T�C�YUP";
        else if (buffInfo.remainDoubleDmgCount > 0) text.text = "�^�_���[�W x2";
        else if (buffInfo.remainFastSpdTime > 0) text.text = "�ړ����xUP";
        else if (buffInfo.remainFastThrowTime > 0) text.text = "���������xUP";
        else if (buffInfo.remainGetItemCT > 0) text.text = "�A�C�e���擾CD(*�L��`*)�����";
        else text.text = "�o�t���擾";
    }
}