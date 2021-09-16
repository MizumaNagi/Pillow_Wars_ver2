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
        if (buffInfo.remainBigPillowTime > 0) text.text = "枕サイズUP";
        else if (buffInfo.remainDoubleDmgCount > 0) text.text = "与ダメージ x2";
        else if (buffInfo.remainFastSpdTime > 0) text.text = "移動速度UP";
        else if (buffInfo.remainFastThrowTime > 0) text.text = "枕投擲速度UP";
        else if (buffInfo.remainGetItemCT > 0) text.text = "アイテム取得CD(*´ε`*)ﾁｭｯﾁｭ";
        else text.text = "バフ未取得";
    }
}