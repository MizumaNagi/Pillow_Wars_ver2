using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField] public int joinPlayers;
    [SerializeField] public RuleData ruleData;

    [System.NonSerialized] public int remainCharacters;
    [System.NonSerialized] public bool isPause = false;

    private bool isPlayTheGame = false;

    private void Start()
    {
        Init();
    }

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

    private void Init()
    {
    }

    public void GameStart()
    {
        isPlayTheGame = true;
        PlayerManager.Instance.Init();
        GameEventScript.Instance.Init();
        remainCharacters = joinPlayers;
    }

    private void GameEnd()
    {
        isPlayTheGame = false;
    }
}
