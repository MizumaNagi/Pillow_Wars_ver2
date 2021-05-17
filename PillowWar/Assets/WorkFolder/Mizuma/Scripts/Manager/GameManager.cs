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
        GameStart();
    }

    private void Update()
    {
        PlayerManager.Instance.UpdateMethod();

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
                InputManager.Instance.MoveInputUpdateMethpod();
            }
            else
            {
                InputManager.Instance.UiInputUpdateMethod();
            }
        }

        if (remainCharacters <= 1) GameEnd();
    }

    private void GameStart()
    {
        Debug.Log("Start");

        isPlayTheGame = true;
        PlayerManager.Instance.Init();
        remainCharacters = joinPlayers;
    }

    private void GameEnd()
    {
        isPlayTheGame = false;
        Debug.Log("End");
    }
}
