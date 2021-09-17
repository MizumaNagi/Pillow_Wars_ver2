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
            $"サイズUP: {buffInfo.remainBigPillowTime}\n" +
            $"与ダメx2: {buffInfo.remainDoubleDmgCount}\n" +
            $"移動速度UP: {buffInfo.remainFastSpdTime}\n" +
            $"投擲速度UP: { buffInfo.remainFastThrowTime}\n" +
            $"CT: {buffInfo.remainGetItemCT}\n";
    }
}