using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoTitle : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManagement.Instance.LoadScene(SCENE_NAME.TITLE);
        }

        for(int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            if(Input.GetButtonDown(InputManager.Instance.playerInput[i].Ok)) SceneManagement.Instance.LoadScene(SCENE_NAME.TITLE);
        }
    }
}
