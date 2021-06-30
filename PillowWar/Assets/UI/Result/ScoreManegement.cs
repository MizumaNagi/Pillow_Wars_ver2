using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManegement : MonoBehaviour
{
    //変数宣言
    // int GamePlayer1;
    // int GamePlayer2;
    // int GamePlayer3;
    // int GamePlayer4;

    int[] GamePlayer;

    //配列
    [SerializeField]
    public Text[] GetTexts;

    void Start()
    {
        System.Array.Resize(ref GamePlayer, GameManager.Instance.joinPlayers);

        //リザルト結果の情報を変数に入れる。
        // GamePlayer1 = GameManager.Instance.resultIDs[3];
        // GamePlayer2 = GameManager.Instance.resultIDs[2];
        // GamePlayer3 = GameManager.Instance.resultIDs[1];
        // GamePlayer4 = GameManager.Instance.resultIDs[0];
        for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            GamePlayer[i] = GameManager.Instance.resultIDs[i];
        }

        string[] rankModifier = { "st", "nd", "rd", "th" };

        //リザルト表示
        //GetTexts[0].text = "1st Player" + GamePlayer[0].ToString();
        //GetTexts[1].text = "2nd Player" + GamePlayer[1].ToString();
        //GetTexts[2].text = "3rd Player" + GamePlayer[2].ToString();
        //GetTexts[3].text = "4th Player" + GamePlayer[3].ToString();
        for(int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            GetTexts[i].text = "";
            GetTexts[i].text = $"{i + 1}{rankModifier[i]}: Player{GamePlayer[i]}";
        }
    }
}
