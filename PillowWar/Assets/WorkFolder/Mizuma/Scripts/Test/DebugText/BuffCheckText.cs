using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(9999)]
[System.Serializable]
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
        text.text = $"\n" +
            $"�T�C�YUP: {buffInfo.remainBigPillowTime}\n" +
            $"�^�_��x2: {buffInfo.remainDoubleDmgCount}\n" +
            $"�ړ����xUP: {buffInfo.remainFastSpdTime}\n" +
            $"�������xUP: { buffInfo.remainFastThrowTime}\n" +
            $"CT: {buffInfo.remainGetItemCT}\n";
    }
}