using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void OnStart()
    {
        //StartBottonを押すとゲームシーンをロードする。
        SceneManagement.Instance.LoadScene(SCENE_NAME.SELECT);
    }
}
