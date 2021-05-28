using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManegement : MonoBehaviour
{
    int GamePlayer1;
    int GamePlayer2;
    int GamePlayer3;
    int GamePlayer4;

    [SerializeField]
    public Text[] GetTexts;

    // Start is called before the first frame update
    void Start()
    {
        GamePlayer1 = GameManager.Instance.resultIDs[3];
        GamePlayer2 = GameManager.Instance.resultIDs[2];
        GamePlayer3 = GameManager.Instance.resultIDs[1];
        GamePlayer4 = GameManager.Instance.resultIDs[0];



        GetTexts[0].text = "1st Player" + GamePlayer1.ToString();
        GetTexts[1].text = "2nd Player" + GamePlayer2.ToString();
        GetTexts[2].text = "3rd Player" + GamePlayer3.ToString();
        GetTexts[3].text = "4th Player" + GamePlayer4.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
