using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField] public int joinPlayers;
    [SerializeField] public RuleData ruleData;

    private void Start()
    {
        PlayerManager.Instance.Init();
    }

    private void Update()
    {
        PlayerManager.Instance.UpdateMethod();
    }
}
