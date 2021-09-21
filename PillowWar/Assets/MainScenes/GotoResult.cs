using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoResult : MonoBehaviour
{
    void Start()
    {
        SceneController.Instance.LoadScene(SCENE_NAME.RESULT);
    }
}
