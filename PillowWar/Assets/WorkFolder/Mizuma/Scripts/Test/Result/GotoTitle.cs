using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoTitle : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneController.Instance.LoadLoadingScene(SCENE_NAME.RESULT, SCENE_NAME.TITLE);
        }

        for(int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            if(Input.GetButtonDown(InputManager.Instance.playerInput[i].Ok)) SceneController.Instance.LoadLoadingScene(SCENE_NAME.RESULT, SCENE_NAME.TITLE);
        }
    }
}
