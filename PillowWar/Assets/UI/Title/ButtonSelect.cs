using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{
    //�I�u�W�F�N�g
    public Button StartSelect;
    public Button ExitSelect;

    void Start()
    {
        //StartButton��������ԂőI����Ԃɂ���B
        StartSelect.Select();
    }
}
