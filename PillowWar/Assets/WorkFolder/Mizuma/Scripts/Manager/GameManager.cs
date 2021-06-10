#pragma warning disable CS0114

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField] public int joinPlayers;
    [SerializeField] public int joinNpcs;
    [SerializeField] public RuleData ruleData;

    [System.NonSerialized] public int remainCharacters;
    [System.NonSerialized] public bool isPause = false;

    public bool isPlayTheGame { get; private set; } = false;
    public List<int> resultIDs = new List<int>();

    private void Awake()
    {
        base.Awake();
        Application.targetFrameRate = 60;
    }

    // 仮リザルト情報入力
    private void Start()
    {
        for (int i = 0; i < joinPlayers; i++) resultIDs.Add(i);
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Game") GameStart();
    }

    private void Update()
    {
        if (isPlayTheGame == false)
        {
            InputManager.Instance.UiInputUpdateMethod();
        }
        else
        {
            InputManager.Instance.GeneralInputUpdateMethod();
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
        FindWinCharacterID();
        PlayerManager.Instance.DataReset();
        SceneManagement.Instance.LoadScene(SCENE_NAME.RESULT);
        isPlayTheGame = false;
    }

    private void Init()
    {
        resultIDs.Clear();
        remainCharacters = joinPlayers + joinNpcs;
    }

    private void FindWinCharacterID()
    {
        var IDLists = new List<int>();
        for(int i = 0; i < joinPlayers; i++)
        {
            IDLists.Add(i);
        }
        for(int i = 0; i < joinNpcs; i++)
        {
            IDLists.Add(-i);
        }

        foreach(int loseID in resultIDs.ToArray())
        {
            IDLists.Remove(loseID);
        }
        if(IDLists.Count != 1)
        {
            Debug.LogError("勝利プレイヤーの検索失敗");
            return;
        }
        resultIDs.Add(IDLists[0]);
    }
}
