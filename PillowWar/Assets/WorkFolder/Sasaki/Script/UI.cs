using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Slider[] hp;
    [SerializeField] private Image[] image;
    [SerializeField] private Text[] Futontext;
    public UnityEngine.UI.Text Pausetext;
    public List<float> playerhp = new List<float>();

    // Start is called before the first frame update
    void Start()
    {
        Pausetext.enabled = false;

        for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            playerhp.Add(0);
        }
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            playerhp[i] = 0;
            playerhp[i] = (float)PlayerManager.Instance.charaDatas[i].HP / (float)GameManager.Instance.ruleData.maxHp;

            hp[i].value = playerhp[i];
            Pause();
            Image();
        }
    }
    public void Pause()
    {
        if (GameManager.Instance.isPause == true)
        {
            Pausetext.enabled = true;
        }
        else
        {
            Pausetext.enabled = false;
        }
    }

    public void Image()
    {
        for (int i = 0; i < image.Length; i++)
        {
            if (PlayerManager.Instance.charaDatas[i].isInBed == true)
            {
                Futontext[i].enabled = true;
                image[i].enabled = true;
            }
            else
            {
                Futontext[i].enabled = false;
                image[i].enabled = false;
            }
        }
    }
}
