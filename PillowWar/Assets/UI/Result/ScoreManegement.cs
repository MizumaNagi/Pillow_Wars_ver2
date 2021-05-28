using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManegement : MonoBehaviour
{
    //�ϐ��錾
    int GamePlayer1;
    int GamePlayer2;
    int GamePlayer3;
    int GamePlayer4;

    //�z��
    [SerializeField]
    public Text[] GetTexts;

    void Start()
    {
        //���U���g���ʂ̏���ϐ��ɓ����B
        GamePlayer1 = GameManager.Instance.resultIDs[3];
        GamePlayer2 = GameManager.Instance.resultIDs[2];
        GamePlayer3 = GameManager.Instance.resultIDs[1];
        GamePlayer4 = GameManager.Instance.resultIDs[0];

        //���U���g�\��
        GetTexts[0].text = "1st Player" + GamePlayer1.ToString();
        GetTexts[1].text = "2nd Player" + GamePlayer2.ToString();
        GetTexts[2].text = "3rd Player" + GamePlayer3.ToString();
        GetTexts[3].text = "4th Player" + GamePlayer4.ToString();

    }
}
