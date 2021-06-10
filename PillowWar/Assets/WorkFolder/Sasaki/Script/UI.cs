using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    //[SerializeField] private Slider[] hp;
    [SerializeField] private Image[] Futontimage;
    [SerializeField] private Text[] Futontext;
    [SerializeField] private Text Titlereturntext;
    [SerializeField] private Text Gamereturntext;
    [SerializeField] private Transform[] hpIconParents;

    public UnityEngine.UI.Text Pausetext;
    //public List<float> playerhp = new List<float>();
    private int iconChild = 0;
    private List<List<Image>> hpIcons = new List<List<Image>>();

    // Start is called before the first frame update
    void Start()
    {
        Pausetext.enabled = false;

        iconChild = hpIconParents[0].childCount;
        hpicon();

        //for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
        //{
        //    playerhp.Add(0);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            float playerhp = (float)PlayerManager.Instance.playerDatas[i].HP / (float)GameManager.Instance.ruleData.maxHp;

            for (int j = 0; j < iconChild; j++)
            {
                hpIcons[i][j].fillAmount = playerhp * iconChild - 1 * j;
            }
        }
        Pause();
        Image();

        //for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
        //{
        //    playerhp[i] = 0;
        //    playerhp[i] = (float)PlayerManager.Instance.charaDatas[i].HP / (float)GameManager.Instance.ruleData.maxHp;

        //    hp[i].value = playerhp[i];
        //}
    }

    public void Pause()
    {
        if (GameManager.Instance.isPause == true)
        {
            Titlereturntext.enabled = true;
            Gamereturntext.enabled = true;
            Pausetext.enabled = true;
        }
        else
        {
            Titlereturntext.enabled = false;
            Gamereturntext.enabled = false;
            Pausetext.enabled = false;
        }
    }

    public void Image()
    {
        for (int i = 0; i < Futontimage.Length; i++)
        {
            if (PlayerManager.Instance.playerDatas[i].isInBed == true)
            {
                Futontext[i].enabled = true;
                Futontimage[i].enabled = true;
            }
            else
            {
                Futontext[i].enabled = false;
                Futontimage[i].enabled = false;
            }
        }
    }

    private void hpicon()
    {
        for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            List<Image> images = new List<Image>();
            for (int j = 0; j < iconChild; j++)
            {
                images.Add(hpIconParents[i].GetChild(j).GetComponent<Image>());
            }
            hpIcons.Add(images);
        }
    }
}
