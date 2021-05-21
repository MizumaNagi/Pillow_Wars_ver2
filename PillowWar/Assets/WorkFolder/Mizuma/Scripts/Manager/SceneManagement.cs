#pragma warning disable 0114

using UnityEngine;
using UnityEngine.SceneManagement;

public enum SCENE_NAME
{
    TITLE,
    GAME,
    RESULT
}

public class SceneManagement : SingletonMonoBehaviour<SceneManagement>
{
    private void Start()
    {
        Debug.Log("Scene/Awake");
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
        if (name == "Game")
        {
            Debug.Log("Scene/OnSceneLoaded");
            GameManager.Instance.GameStart();
        }
        if (name == "Result")
        {
        }
    }

    private void OnSceneUnloaded(Scene unloadScene)
    {
        string name = unloadScene.name;
        if (name == "Title")
        {
        }
        if (name == "Game")
        {
        }
        if (name == "Result")
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
