#pragma warning disable CS0114

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField, Range(1,4)] public int joinPlayers;
    [SerializeField, Range(0,4)] public int joinNpcs;
    [SerializeField] public RuleData ruleData;

    public int remainPlayers;
    public int remainCharacters;
    public bool isPause = false;

    public bool isPlayTheGame { get; private set; } = false;
    public List<int> resultIDs = new List<int>();

    private void Awake()
    {
        base.Awake();
        Application.targetFrameRate = 60;
    }

    // �����U���g������
    private void Start()
    {
        for (int i = 0; i < joinPlayers; i++) resultIDs.Add(i);
        //if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Game") GameStart();
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

            if (remainPlayers <= 0 || remainCharacters <= 1) GoResult();
        }
    }

    public IEnumerator DelayGameStart(float delayTime)
    {
        PlayerManager.Instance.Init();
        GameEventScript.Instance.Init();
        BedManager.Instance.RandomObjActive();
        Init();
        yield return new WaitForSeconds(delayTime);
        isPlayTheGame = true;
    }

    private void GameStart()
    {
        isPlayTheGame = true;
        PlayerManager.Instance.Init();
        GameEventScript.Instance.Init();
        BedManager.Instance.RandomObjActive();
        Init();
    }

    public void GameEnd()
    {
        PlayerManager.Instance.DataReset();
        GameEventScript.Instance.StatusReset();
        BedManager.Instance.AllBedChgActive(false);
        isPlayTheGame = false;
        isPause = false;
    }

    private void GoResult()
    {
        FindWinCharacterID();
        GameEnd();
        SceneController.Instance.LoadScene(SCENE_NAME.RESULT);
    }

    private void Init()
    {
        resultIDs.Clear();
        remainPlayers = joinPlayers;
        remainCharacters = joinPlayers + joinNpcs;
    }

    private void FindWinCharacterID()
    {
        if(resultIDs.Count != joinPlayers)
        {
            var IDLists = new List<int>();
            for (int i = 0; i < joinPlayers; i++)
            {
                IDLists.Add(i + 1);
            }
            //for(int i = 0; i < joinNpcs; i++)
            //{
            //    IDLists.Add(-i - 1);
            //}

            foreach (int loseID in resultIDs.ToArray())
            {
                IDLists.Remove(loseID);
            }
            if (IDLists.Count != 1)
            {
                Debug.LogError("�����v���C���[�̌������s");
                foreach (int i in IDLists.ToArray())
                {
                    Debug.Log(i);
                }
                return;
            }
            resultIDs.Add(IDLists[0]);
        }
    }
}
