using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField] public int joinPlayers;
    [SerializeField] public RuleData ruleData;

    protected override void Awake()
    {
        base.Awake();
        PlayerManager.Instance.Init();
    }
}
