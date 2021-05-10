using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/InputData", order = 1)]
public class InputData : ScriptableObject
{
    [SerializeField] private int playerNo;

    [Header("ゲーム中操作")]
    [SerializeField] private XboxConAllTypeEnum moveX = XboxConAllTypeEnum.Not_Use;          // 移動 (座標X)
    [SerializeField] private XboxConAllTypeEnum moveY = XboxConAllTypeEnum.Not_Use;          // 移動 (座標Y)
    [SerializeField] private XboxConAllTypeEnum viewpointMoveX = XboxConAllTypeEnum.Not_Use; // 座標移動 (座標X)
    [SerializeField] private XboxConAllTypeEnum viewpointMoveY = XboxConAllTypeEnum.Not_Use; // 座標移動 (座標Y)
                                 
    [SerializeField] private XboxConAllTypeEnum run = XboxConAllTypeEnum.Not_Use;           // 走る
    [SerializeField] private XboxConAllTypeEnum squat = XboxConAllTypeEnum.Not_Use;         // しゃがむ
    [SerializeField] private XboxConAllTypeEnum jump = XboxConAllTypeEnum.Not_Use;          // ジャンプ
    [SerializeField] private XboxConAllTypeEnum interact = XboxConAllTypeEnum.Not_Use;      // インタラクト
    [SerializeField] private XboxConAllTypeEnum switchToADS = XboxConAllTypeEnum.Not_Use;   // ADS状態へ移行
    [SerializeField] private XboxConAllTypeEnum pillowThrow = XboxConAllTypeEnum.Not_Use;   // 枕投げ
    [SerializeField] private XboxConAllTypeEnum option = XboxConAllTypeEnum.Not_Use;        // オプション画面

    [Header("ゲーム外操作")]
    [SerializeField] private XboxConAllTypeEnum cursolMoveX = XboxConAllTypeEnum.Not_Use;
    [SerializeField] private XboxConAllTypeEnum cursolMoveY = XboxConAllTypeEnum.Not_Use;
    //[SerializeField] private XboxConAllTypeEnum up = XboxConAllTypeEnum.Not_Use;      // 上移動
    //[SerializeField] private XboxConAllTypeEnum down = XboxConAllTypeEnum.Not_Use;    // 下移動
    //[SerializeField] private XboxConAllTypeEnum left = XboxConAllTypeEnum.Not_Use;    // 左移動
    //[SerializeField] private XboxConAllTypeEnum right = XboxConAllTypeEnum.Not_Use;   // 右移動

    [SerializeField] private XboxConAllTypeEnum ok = XboxConAllTypeEnum.Not_Use;      // 決定
    [SerializeField] private XboxConAllTypeEnum cancel = XboxConAllTypeEnum.Not_Use;  // キャンセル

    //[Header("ゲーム中操作")]
    //[Header("キーボード操作(テスト用)")]
    //[SerializeField] private KeyCode key_moveX;          // 移動 (座標X)
    //[SerializeField] private KeyCode key_moveY;          // 移動 (座標Y)
    //[SerializeField] private string key_viewpointMoveX = ; // 座標移動 (座標X)
    //[SerializeField] private string key_viewpointMoveY = ; // 座標移動 (座標Y)
    //
    //[SerializeField] private KeyCode key_run;           // 走る
    //[SerializeField] private KeyCode key_squat;         // しゃがむ
    //[SerializeField] private KeyCode key_jump;          // ジャンプ
    //[SerializeField] private KeyCode key_interact;      // インタラクト
    //[SerializeField] private KeyCode key_switchToADS;   // ADS状態へ移行
    //[SerializeField] private KeyCode key_pillowThrow;   // 枕投げ
    //[SerializeField] private KeyCode key_option;        // オプション画面
    //
    //[Header("ゲーム外操作")]
    //[SerializeField] private KeyCode key_up_down;      // 上下移動
    //[SerializeField] private KeyCode key_left_right;   // 左右移動
    //
    //[SerializeField] private KeyCode key_ok;           // 決定
    //[SerializeField] private KeyCode key_cancel;       // キャンセル

    public string MoveX { get => GetKeyName(moveX); }
    public string MoveY { get => GetKeyName(moveY); }
    public string ViewpointMoveX { get => GetKeyName(viewpointMoveX); }
    public string ViewpointMoveY { get => GetKeyName(viewpointMoveY); }
    public string Run { get => GetKeyName(run); }
    public string Squat { get => GetKeyName(squat); }
    public string Jump { get => GetKeyName(jump); }
    public string SwitchToADS { get => GetKeyName(switchToADS); }
    public string Interact { get => GetKeyName(interact); }
    public string PillowThrow { get => GetKeyName(pillowThrow); }

    public string Option { get => GetKeyName(option); }
    public string CursolMoveX { get => GetKeyName(cursolMoveX); }
    public string CursolMoveY { get => GetKeyName(cursolMoveY); }
    //public string Up { get => GetKeyName(up); }
    //public string Down { get => GetKeyName(down); }
    //public string Left { get => GetKeyName(left); }
    //public string Right { get => GetKeyName(right); }
    public string Ok { get => GetKeyName(ok); }
    public string Cancel { get => GetKeyName(cancel); }

    private string GetKeyName<T>(T key)
    {
        return $"{Enum.GetName(typeof(T), key)}_P{this.playerNo}";
    }
}
