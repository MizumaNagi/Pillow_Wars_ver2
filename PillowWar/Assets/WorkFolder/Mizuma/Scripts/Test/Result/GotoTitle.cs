using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoTitle : MonoBehaviour
{
    private float delayTime = 0f;
    private float canGoToTime = 6f;

    void Update()
    {
        delayTime += Time.deltaTime;
        if (delayTime < canGoToTime) return;

        if (Input.GetKeyDown(KeyCode.Space) && delayTime > canGoToTime)
        {
            SceneController.Instance.LoadLoadingScene(SCENE_NAME.RESULT, SCENE_NAME.TITLE);
        }

        for(int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {

            if(Input.GetButtonDown(InputManager.Instance.playerInput[i].Ok)) SceneController.Instance.LoadLoadingScene(SCENE_NAME.RESULT, SCENE_NAME.TITLE);
        }

    }
}
