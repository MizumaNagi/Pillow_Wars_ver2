using UnityEngine;
using UnityEngine.SceneManagement;

public class TestSceneLoader : MonoBehaviour
{
    void Update()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown(InputManager.Instance.playerInput[0].Ok))
        {
            if (sceneName == "Title") SceneController.Instance.LoadLoadingScene(SCENE_NAME.TITLE, SCENE_NAME.GAME);
            else if (sceneName == "Result") SceneController.Instance.LoadLoadingScene(SCENE_NAME.RESULT, SCENE_NAME.TITLE);
            else Debug.LogError("Title,Resultシーンに配置");
        }
    }
}
