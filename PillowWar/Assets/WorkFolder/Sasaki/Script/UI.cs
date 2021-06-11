using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    //[SerializeField] private Slider[] hp;
    [SerializeField] private Image[] Futontimage;
    [SerializeField] private Text[] Futontext;
    [SerializeField] private Transform[] hpIconParents;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button titleButton;

    public UnityEngine.UI.Text Pausetext;
    //public List<float> playerhp = new List<float>();
    private int iconChild = 0;
    private List<List<Image>> hpIcons = new List<List<Image>>();
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;

        resumeButton.onClick.AddListener(PauseResume);
        titleButton.onClick.AddListener(PauseTitle);

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

    private void PauseTitle()
    {
        pausePanel.SetActive(false);
        Pausetext.enabled = false;
        GameManager.Instance.GameEnd();
        SceneManagement.Instance.LoadScene(SCENE_NAME.TITLE);
    }

    private void PauseResume()
    {
        gameManager.isPause = !gameManager.isPause;
    }

    public void Pause()
    {
        if (GameManager.Instance.isPause == true)
        {
            pausePanel.SetActive(true);
            Pausetext.enabled = true;
        }
        else
        {
            pausePanel.SetActive(false);
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