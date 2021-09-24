using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{    
    [SerializeField] private Button PauseButton;
    [SerializeField] private Button exitButton;
    private int selectNum;

    private void Update()
    {
        for(int i = 0; i < 4; i++)
        {
            if(Input.GetAxis(InputManager.Instance.playerInput[i].MoveY) > 0.4f)
            {
                selectNum = 1;
            }
            else if(Input.GetAxis(InputManager.Instance.playerInput[i].MoveY) < -0.4f)
            {
                selectNum = 0;
            }
        }

        if (selectNum == 0) PauseButton.Select();
        else exitButton.Select();
    }

    void OnEnable()
    {
        selectNum = 0;
    }
}
