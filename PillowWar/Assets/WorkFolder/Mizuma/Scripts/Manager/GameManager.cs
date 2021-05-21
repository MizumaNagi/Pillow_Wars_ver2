using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField] public int joinPlayers;
    [SerializeField] public RuleData ruleData;

    [System.NonSerialized] public int remainCharacters;
    [System.NonSerialized] public bool isPause = false;

    public bool isPlayTheGame { get; private set; } = false;
    public List<int> resultIDs = new List<int>();

    private void Update()
    {
        InputManager.Instance.UpdateMethod();
        InputManager.Instance.GeneralInputUpdateMethod();
        if (isPlayTheGame == false)
        {
            InputManager.Instance.UiInputUpdateMethod();
        }
        else
        {
            if (isPause == false)
            {
                InputManager.Instance.MoveInputUpdateMethod();
                PlayerManager.Instance.UpdateMethod();
                GameEventScript.Instance.UpdateMethod();
            }
            else
            {
                InputManager.Instance.UiInputUpdateMethod();
            }

            if (remainCharacters <= 1) GameEnd();
        }
    }

    public void GameStart()
    {
        isPlayTheGame = true;
        PlayerManager.Instance.Init();
        GameEventScript.Instance.Init();
        Init();
    }

    private void GameEnd()
    {
        SceneManagement.Instance.LoadScene(SCENE_NAME.RESULT);
        isPlayTheGame = false;
    }

    private void Init()
    {
        resultIDs.Clear();
        remainCharacters = joinPlayers;
    }
}
