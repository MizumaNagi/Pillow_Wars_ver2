using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPGauge : MonoBehaviour
{
    // 各プレイヤーのHPIconを纏める親Objects
    [SerializeField] private Transform[] hpIconParents;

    // HPアイコンの数(全プレイヤーが同数な前提)
    private int iconChildCount = 0;

    // 参加人数(画面分割数)
    private int joinPlayers = 0;

    // 全プレイヤーの全アイコンImageを格納する
    // List<Image>型のList そもそもListとは...?となったら "C# List"で検索
    // hpIcons[0][3] ←こんな感じで取得(1(0)番目のプレイヤーの4(3)番目のHpIconを取得)
    private List<List<Image>> hpIcons = new List<List<Image>>();

    private void Start()
    {
        iconChildCount = hpIconParents[0].childCount;
        joinPlayers = GameManager.Instance.joinPlayers;

        SetHpIcons();
    }

    private void Update()
    {
        // ポーズ中はHPUIの更新を行わない
        if (GameManager.Instance.isPause == true) return;

        for(int i = 0; i < joinPlayers; i++)
        {
            // i 番目のPlayerの現在HP割合を算出する
            float hpPercentage = (float)PlayerManager.Instance.charaDatas[i].HP / (float)GameManager.Instance.ruleData.maxHp;

            // HP割合をもとにIconを削る
            for(int j = 0; j < iconChildCount; j++)
            {
                hpIcons[i][j].fillAmount = hpPercentage * iconChildCount - 1 * j;
            }
        }
    }

    // HpIconを取得して格納する
    private void SetHpIcons()
    {
        for(int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            // hpIconsにAddする新規List<Image>を用意する
            List<Image> imgs = new List<Image>();

            // アイコン数分ループしてGetComponentでImageを変数に入れていく
            for(int j = 0; j < iconChildCount; j++)
            {
                // 非常に重たくなる可能性有、要改善
                imgs.Add(hpIconParents[i].GetChild(j).GetComponent<Image>());
            }
            // さっき新規で作ったListにImageを入れ終わったのでそのListをhpIconsにAddする
            hpIcons.Add(imgs);
        }
    }
}
