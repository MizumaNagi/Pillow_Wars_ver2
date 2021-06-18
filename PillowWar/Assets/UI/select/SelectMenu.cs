using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMenu : MonoBehaviour
{
    //オブジェクト
    public Button PlayLeftArrowSelect;
    public Button PlayRightArrowSelect;
    public Button StageLeftArrowSelect;
    public Button StageRightArrowSelect;

    Text text;

    void Start()
    {
        //StartButtonを初期状態で選択状態にする。
        PlayLeftArrowSelect.Select();

        text = GetComponent<Text>();
    }

    void FontChangeColor()
    {
        text.color = new Color(255.0f, 140.0f, 0.0f);
    }

}
