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

        //StartBotton�������ƃQ�[���V�[�������[�h����B
        SceneController.Instance.LoadScene(SCENE_NAME.SELECT);
    }
}
