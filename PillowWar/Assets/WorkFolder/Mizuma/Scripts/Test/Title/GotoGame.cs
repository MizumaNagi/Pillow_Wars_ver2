using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoGame : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) SceneManagement.Instance.LoadScene(SCENE_NAME.GAME);
    }
}
