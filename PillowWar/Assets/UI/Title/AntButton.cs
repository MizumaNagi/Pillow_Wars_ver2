using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntButton : MonoBehaviour
{
    [SerializeField] private bool isSkipSelectScene;

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey)
        {
            if(isSkipSelectScene) UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
            else UnityEngine.SceneManagement.SceneManager.LoadScene("Select");
        }
    }
}
