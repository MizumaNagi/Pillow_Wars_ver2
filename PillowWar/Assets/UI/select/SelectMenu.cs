using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMenu : MonoBehaviour
{
    //�I�u�W�F�N�g
    public Button PlayLeftArrowSelect;
    public Button PlayRightArrowSelect;
    public Button StageLeftArrowSelect;
    public Button StageRightArrowSelect;
    public Button playSelect;
    public Button stageSelect;

    Text text;

    void Start()
    {
        //StartButton��������ԂőI����Ԃɂ���B
        playSelect.Select();
    }

}
