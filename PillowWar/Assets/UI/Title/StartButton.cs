using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void OnStart()
    {
        //StartBotton�������ƃQ�[���V�[�������[�h����B
        SceneManagement.Instance.LoadScene(SCENE_NAME.SELECT);
    }
}
