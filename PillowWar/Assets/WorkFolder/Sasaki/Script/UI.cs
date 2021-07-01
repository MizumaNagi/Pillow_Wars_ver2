using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private Image[] FutontimagePlayer4;
    [SerializeField] private Image[] FutontimagePlayer2;
    [SerializeField] private Text[] FutontextPlayer4;
    [SerializeField] private Text[] FutontextPlayer2;
    [SerializeField] private Transform[] hpIconParents;
    [SerializeField] private Transform[] hpIconParents1;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button titleButton;
    [SerializeField] private Image[] Futont1imagePlayer4;
    [SerializeField] private Image[] Futont1imagePlayer2;
    [SerializeField] private Image[] Futont2imagePlayer4;
    [SerializeField] private Image[] Futont2imagePlayer2;

    public UnityEngine.UI.Text Pausetext;
    private int iconChild = 0;
    private List<List<Image>> hpIcons = new List<List<Image>>();
    private List<List<Image>> hpIcons1 = new List<List<Image>>();
    private GameManager gameManager;
    private GameEventScript gameEventScript;
    [SerializeField] private GameObject Player2;
    [SerializeField] private GameObject Player4;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        gameEventScript = GameEventScript.Instance;

        resumeButton.onClick.AddListener(PauseResume);
        titleButton.onClick.AddListener(PauseTitle);

        Pausetext.enabled = false;

        iconChild = hpIconParents[0].childCount;
        iconChild = hpIconParents1[0].childCount;
        hpicon();
        hpicon1();

        // 参加人数の取得方法
        //GameManager.Instance.joinPlayers;

        // もしも参加人数が2人なら...
        if (GameManager.Instance.joinPlayers == 2)
        {
            Player2.SetActive(true);
            Player4.SetActive(false);
        }
        else
        {
            Player2.SetActive(false);
            Player4.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isPlayTheGame == true)
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
            Image1();

            for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
            {
                float playerhp = (float)PlayerManager.Instance.playerDatas[i].HP / (float)GameManager.Instance.ruleData.maxHp;

                for (int j = 0; j < iconChild; j++)
                {
                    hpIcons1[i][j].fillAmount = playerhp * iconChild - 1 * j;
                }
            }
        }

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
        for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            if (PlayerManager.Instance.playerDatas[i].isInBedRange == true && GameEventScript.Instance.canBedIn == true)
            {
                Futont1imagePlayer4[i].enabled = true;
                Futont2imagePlayer4[i].enabled = true;
            }
            else
            {
                Futont1imagePlayer4[i].enabled = false;
                Futont2imagePlayer4[i].enabled = false;
            }
            if (PlayerManager.Instance.playerDatas[i].isInBed == true)
            {
                FutontextPlayer4[i].enabled = true;
                FutontimagePlayer4[i].enabled = true;
                Futont1imagePlayer4[i].enabled = false;
                Futont2imagePlayer4[i].enabled = false;
            }
            else
            {
                FutontextPlayer4[i].enabled = false;
                FutontimagePlayer4[i].enabled = false;
            }
        }
    }

    public void Image1()
    {
        for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            if (PlayerManager.Instance.playerDatas[i].isInBedRange == true && GameEventScript.Instance.canBedIn == true)
            {
                Futont1imagePlayer2[i].enabled = true;
                Futont2imagePlayer2[i].enabled = true;
            }
            else
            {
                Futont1imagePlayer2[i].enabled = false;
                Futont2imagePlayer2[i].enabled = false;
            }
            if (PlayerManager.Instance.playerDatas[i].isInBed == true)
            {
                FutontextPlayer2[i].enabled = true;
                FutontimagePlayer2[i].enabled = true;
                Futont1imagePlayer2[i].enabled = false;
                Futont2imagePlayer2[i].enabled = false;
            }
            else
            {
                FutontextPlayer2[i].enabled = false;
                FutontimagePlayer2[i].enabled = false;
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
    private void hpicon1()
    {
        for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            List<Image> images = new List<Image>();
            for (int j = 0; j < iconChild; j++)
            {
                images.Add(hpIconParents1[i].GetChild(j).GetComponent<Image>());
            }
            hpIcons1.Add(images);
        }
    }
}