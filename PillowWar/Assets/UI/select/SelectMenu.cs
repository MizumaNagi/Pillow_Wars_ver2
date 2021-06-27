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
    public Button playSelect;
    public Button stageSelect;

    Text text;

    void Start()
    {
        //StartButtonを初期状態で選択状態にする。
        playSelect.Select();
    }

}
