#pragma warning disable 0114

using UnityEngine;
using UnityEngine.SceneManagement;

public enum SCENE_NAME
{
    TITLE,
    SELECT,
    GAME,
    RESULT
}

public class SceneController : SingletonMonoBehaviour<SceneController>
{
    private void Start()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnSceneLoaded(Scene loadScene, LoadSceneMode mode)
    {
        string name = loadScene.name;
        if(name == "Title")
        {
        }
        else if (name == "Select")
        {
        }
        else if (name == "Game")
        {
            GameManager.Instance.GameStart();
        }
        else if (name == "Result")
        {
        }
        else
        {
        }
    }

    private void OnSceneUnloaded(Scene unloadScene)
    {
        string name = unloadScene.name;
        if (name == "Title")
        {
        }
        else if (name == "Select")
        {
        }
        else if (name == "Game")
        {
        }
        else if (name == "Result")
        {
        }
        else
        {
        }
    }

    public void LoadScene(SCENE_NAME name)
    {
        switch(name)
        {
            case SCENE_NAME.TITLE:
                {
                    SceneManager.LoadScene("Title");
                    break;
                }
            case SCENE_NAME.SELECT:
                {
                    SceneManager.LoadScene("Select");
                    break;
                }
            case SCENE_NAME.GAME:
                {
                    SceneManager.LoadScene("Game");
                    break;
                }
            case SCENE_NAME.RESULT:
                {
                    SceneManager.LoadScene("Result");
                    break;
                }
        }
    }
}
