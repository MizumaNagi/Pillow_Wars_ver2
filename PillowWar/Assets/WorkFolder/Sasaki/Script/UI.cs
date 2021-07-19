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
    [SerializeField] private Text[] Futon1textPlayer2;
    [SerializeField] private Text[] Futon1textPlayer4;


    [SerializeField] private Image[] hpimagePlayer2;
    [SerializeField] private Image[] hpimage1Player2;
    [SerializeField] private Image[] hpimage2Player2;
    [SerializeField] private Image[] hpimage3Player2;
    [SerializeField] private Image[] hpimage4Player2;
    [SerializeField] private Image[] hpimagePlayer4;
    [SerializeField] private Image[] hpimage1Player4;
    [SerializeField] private Image[] hpimage2Player4;
    [SerializeField] private Image[] hpimage3Player4;
    [SerializeField] private Image[] hpimage4Player4;

    public UnityEngine.UI.Text Pausetext;
    public int iconChild = 0;
    float[] futonhp = new float[4];
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


        // 参加人数の取得方法
        //GameManager.Instance.joinPlayers;

        // もしも参加人数が2人なら...
        if (GameManager.Instance.joinPlayers == 2)
        {
            Player2.SetActive(true);
            Player4.SetActive(false);
            iconChild = hpIconParents1[0].childCount;
            hpicon1();
        }
        else
        {
            Player2.SetActive(false);
            Player4.SetActive(true);
            iconChild = hpIconParents[0].childCount;
            hpicon();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.Instance.isPlayTheGame == true)
        {
            Futonhpvalue();
            // もし参加人数が2人なら...
            if (GameManager.Instance.joinPlayers == 2)
            {
                Image1();

                for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
                {
                    float playerhp = (float)PlayerManager.Instance.playerDatas[i].HP / (float)GameManager.Instance.ruleData.maxHp;

                    for (int j = 0; j < iconChild; j++)
                    {
                        hpIcons1[i][j].fillAmount = playerhp * iconChild - 1 * j;
                    }

                    if (PlayerManager.Instance.playerDatas[i].HP == 10)
                    {
                        hpimagePlayer2[i].enabled = true;
                        hpimage1Player2[i].enabled = false;
                        hpimage2Player2[i].enabled = false;
                        hpimage3Player2[i].enabled = false;
                        hpimage4Player2[i].enabled = false;
                    }
                    if (PlayerManager.Instance.playerDatas[i].HP < 8)
                    {
                        hpimage1Player2[i].enabled = true;
                        hpimagePlayer2[i].enabled = false;
                    }
                    if (PlayerManager.Instance.playerDatas[i].HP < 5)
                    {
                        hpimage2Player2[i].enabled = true;
                        hpimage1Player2[i].enabled = false;
                    }
                    if (PlayerManager.Instance.playerDatas[i].HP < 2)
                    {
                        hpimage3Player2[i].enabled = true;
                        hpimage2Player2[i].enabled = false;
                    }
                    if (PlayerManager.Instance.playerDatas[i].HP < 1)
                    {
                        hpimage4Player2[i].enabled = true;
                        hpimage3Player2[i].enabled = false;
                    }
                }
            }
            else
            {
                Image();

                for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
                {
                    float playerhp = (float)PlayerManager.Instance.playerDatas[i].HP / (float)GameManager.Instance.ruleData.maxHp;

                    for (int j = 0; j < iconChild; j++)
                    {
                        hpIcons[i][j].fillAmount = playerhp * iconChild - 1 * j;
                    }
                    if (PlayerManager.Instance.playerDatas[i].HP == 10)
                    {
                        hpimagePlayer4[i].enabled = true;
                        hpimage1Player4[i].enabled = false;
                        hpimage2Player4[i].enabled = false;
                        hpimage3Player4[i].enabled = false;
                        hpimage4Player4[i].enabled = false;
                    }
                    if (PlayerManager.Instance.playerDatas[i].HP < 8)
                    {
                        hpimage1Player4[i].enabled = true;
                        hpimagePlayer4[i].enabled = false;
                    }
                    if (PlayerManager.Instance.playerDatas[i].HP < 5)
                    {
                        hpimage2Player4[i].enabled = true;
                        hpimage1Player4[i].enabled = false;
                    }
                    if (PlayerManager.Instance.playerDatas[i].HP < 2)
                    {
                        hpimage3Player4[i].enabled = true;
                        hpimage2Player4[i].enabled = false;
                    }
                    if (PlayerManager.Instance.playerDatas[i].HP < 1)
                    {
                        hpimage4Player4[i].enabled = true;
                        hpimage3Player4[i].enabled = false;
                    }
                }
            }

            Pause();
        }
    }

    private void PauseTitle()
    {
        pausePanel.SetActive(false);
        Pausetext.enabled = false;
        GameManager.Instance.GameEnd();
        SceneController.Instance.LoadScene(SCENE_NAME.TITLE);
        Time.timeScale = 1;
    }
    private void Futonhpvalue()
    {
        if (GameManager.Instance.joinPlayers == 2)
        {
            for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
            {
                if (PlayerManager.Instance.playerDatas[i].bedStatus == null) continue;

                futonhp[i] = PlayerManager.Instance.playerDatas[i].bedStatus.remainDamagetime / GameManager.Instance.ruleData.inBedDamageTime;
                Futon1textPlayer2[i].text = "眠気 " + Mathf.Floor((1 - futonhp[i]) * 100) + "%";
            }
        }
        else
        {
            for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
            {
                if (PlayerManager.Instance.playerDatas[i].bedStatus == null) continue;

                futonhp[i] = PlayerManager.Instance.playerDatas[i].bedStatus.remainDamagetime / GameManager.Instance.ruleData.inBedDamageTime;
                Futon1textPlayer4[i].text = "眠気 " + Mathf.Floor((1 - futonhp[i]) * 100) + "%";
            }

        }
    }

    private void PauseResume()
    {
        gameManager.isPause = !gameManager.isPause;
        Time.timeScale = 1;
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
                Futon1textPlayer4[i].enabled = true;
                FutontextPlayer4[i].enabled = true;
                FutontimagePlayer4[i].enabled = true;
                Futont1imagePlayer4[i].enabled = false;
                Futont2imagePlayer4[i].enabled = false;
            }
            else
            {
                Futon1textPlayer4[i].enabled = false;
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
                Futon1textPlayer2[i].enabled = true;

                FutontextPlayer2[i].enabled = true;
                FutontimagePlayer2[i].enabled = true;
                Futont1imagePlayer2[i].enabled = false;
                Futont2imagePlayer2[i].enabled = false;
            }
            else
            {
                Futon1textPlayer2[i].enabled = false;
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