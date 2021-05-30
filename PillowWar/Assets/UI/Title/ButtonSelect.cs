using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{
    //オブジェクト
    public Button StartSelect;
    public Button ExitSelect;

    void Start()
    {
        //StartButtonを初期状態で選択状態にする。
        StartSelect.Select();
    }
}
