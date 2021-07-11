using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [SerializeField] private bool goToGame;

    public void OnStart()
    {
        if (goToGame)
        {
            SceneController.Instance.LoadScene(SCENE_NAME.GAME);
            return;
        }

        //StartBottonを押すとゲームシーンをロードする。
        SceneController.Instance.LoadScene(SCENE_NAME.SELECT);
    }
}
