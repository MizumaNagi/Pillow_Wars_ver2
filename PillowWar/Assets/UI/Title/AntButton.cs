using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntButton : MonoBehaviour
{
    [SerializeField] private bool isSkipSelectScene;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (isSkipSelectScene) SceneController.Instance.LoadLoadingScene(SCENE_NAME.TITLE, SCENE_NAME.GAME);
            else SceneController.Instance.LoadLoadingScene(SCENE_NAME.TITLE, SCENE_NAME.SELECT);
        }
    }
}
