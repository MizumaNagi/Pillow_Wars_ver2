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
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;

        AudioManager.Instance.BGMPlay(BGMName.Title);
    }

    private void OnSceneLoaded(Scene loadScene, LoadSceneMode mode)
    {
        string name = loadScene.name;

        if(name == "Title")
        {
            AudioManager.Instance.BGMPlay(BGMName.Title);
        }
        else if (name == "Select")
        {
            AudioManager.Instance.BGMPlay(BGMName.Select);
        }
        else if (name == "Game")
        {
            AudioManager.Instance.BGMPlay(BGMName.Main);
            StartCoroutine(GameManager.Instance.DelayGameStart(3f));
        }
        else if (name == "Result")
        {
            AudioManager.Instance.BGMPlay(BGMName.Result);
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
