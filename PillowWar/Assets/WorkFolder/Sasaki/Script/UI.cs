using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class UI : MonoBehaviour
{
    [SerializeField] private Image[] FutontimagePlayer4;
    [SerializeField] private Image[] FutontimagePlayer2;
    [SerializeField] private Image[] Futont3imagePlayer2;
    [SerializeField] private Image[] Futont3imagePlayer4;
    [SerializeField] private Text[] FutontextPlayer4;
    [SerializeField] private Text[] FutontextPlayer2;
    [SerializeField] private Transform[] hpIconParents;
    //[SerializeField] private Transform[] hpIconParents1;
    [SerializeField] private Transform[] hpIconParents2;
    //[SerializeField] private Transform[] hpIconParents3;
    //[SerializeField] private Transform[] hpIconParents4;
    //[SerializeField] private Transform[] hpIconParents5;
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

    [SerializeField] private GameObject[] damageUi2;
    [SerializeField] private GameObject[] damageUi4;

    [SerializeField] private Image[] hpIconsPlayer1;
    [SerializeField] private Image[] hpIconsPlayer2;
    [SerializeField] private Image[] hpIconsPlayer3;
    [SerializeField] private Image[] hpIconsPlayer4;
    [SerializeField] private Image[] hpIconsPlayerA;
    [SerializeField] private Image[] hpIconsPlayerB;

    public Text Pausetext;
    public int iconChild = 10;
    float[] futonhp = new float[4];
    private List<List<Image>> hpIcons = new List<List<Image>>();
    //private List<List<Image>> hpIcons1 = new List<List<Image>>();
    private List<List<Image>> hpIcons2 = new List<List<Image>>();
    //private List<List<Image>> hpIcons3 = new List<List<Image>>();
    //private List<List<Image>> hpIcons4 = new List<List<Image>>();
    //private List<List<Image>> hpIcons5 = new List<List<Image>>();
    private GameManager gameManager;
    [SerializeField] private GameObject Player2;
    [SerializeField] private GameObject Player4;


    // Start is called before the first frame update
    void Start()
    {
        joinPlayerNum = GameManager.Instance.joinPlayers;

        SetHpIcons();

        gameManager = GameManager.Instance;

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
            //iconChild = hpIconParents1[0].childCount;
            //iconChild = hpIconParents4[0].childCount;
            //iconChild = hpIconParents5[0].childCount;
            //hpicon1();
            //hpicon4();
            //hpicon5();
            hpicon();
        }
        else
        {
            Player2.SetActive(false);
            Player4.SetActive(true);
            //iconChild = hpIconParents[0].childCount;
            //iconChild = hpIconParents2[0].childCount;
            //iconChild = hpIconParents3[0].childCount;
            //hpicon();
            //hpicon3();
            hpicon2();
        }

    }

    // Update is called once per frame
    void Update()
    {
        ChangeHpEvent();

        if (GameManager.Instance.isPlayTheGame == true)
        {
            Futonhpvalue();
            // もし参加人数が2人なら...
            if (GameManager.Instance.joinPlayers == 2)
            {
                Image1();

                float playerhpA = (float)PlayerManager.Instance.playerDatas[0].HP / (float)GameManager.Instance.ruleData.maxHp;
                float playerhpB = (float)PlayerManager.Instance.playerDatas[1].HP / (float)GameManager.Instance.ruleData.maxHp;

                for (int i = 0; i < 10; i++)
                {
                    hpIconsPlayerA[i].fillAmount = playerhpA * 10 - 1 * i;
                    hpIconsPlayerB[i].fillAmount = playerhpB * 10 - 1 * i;
                }

                for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
                {
                    // float playerhp = (float)PlayerManager.Instance.playerDatas[i].HP / (float)GameManager.Instance.ruleData.maxHp;
                    // int playerHp = PlayerManager.Instance.playerDatas[i].HP;
                    // 
                    // for (int j = 0; j < iconChild; j++)
                    // {
                    //     if (playerHp >= j) hpIcons[i][j].fillAmount = 0;
                    //     else hpIcons[i][j].fillAmount = 1;
                    // 
                    //     hpIcons[i][j].fillAmount = playerhp * iconChild - 1 * j;
                    //     hpIcons1[i][j].fillAmount = playerhp * iconChild - 1 * j;
                    //     hpIcons4[i][j].fillAmount = playerhp * iconChild - 1 * j;
                    //     hpIcons5[i][j].fillAmount = playerhp * iconChild - 1 * j;
                    // }

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

                float playerhp1 = (float)PlayerManager.Instance.playerDatas[0].HP / (float)GameManager.Instance.ruleData.maxHp;
                float playerhp2 = (float)PlayerManager.Instance.playerDatas[1].HP / (float)GameManager.Instance.ruleData.maxHp;
                float playerhp3 = (float)PlayerManager.Instance.playerDatas[2].HP / (float)GameManager.Instance.ruleData.maxHp;
                float playerhp4 = (float)PlayerManager.Instance.playerDatas[3].HP / (float)GameManager.Instance.ruleData.maxHp;

                for (int i = 0; i < 10; i++)
                {
                    hpIconsPlayer1[i].fillAmount = playerhp1 * 10 - 1 * i;
                    hpIconsPlayer2[i].fillAmount = playerhp2 * 10 - 1 * i;
                    hpIconsPlayer3[i].fillAmount = playerhp3 * 10 - 1 * i;
                    hpIconsPlayer4[i].fillAmount = playerhp4 * 10 - 1 * i;
                }

                for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
                {
                    // float playerhp = (float)PlayerManager.Instance.playerDatas[i].HP / (float)GameManager.Instance.ruleData.maxHp;
                    // int playerHp = PlayerManager.Instance.playerDatas[i].HP;
                    // 
                    // for (int j = 0; j < iconChild; j++)
                    // {
                    //     if (playerHp >= j) hpIcons2[i][j].fillAmount = 0;
                    //     else hpIcons2[i][j].fillAmount = 1;
                    //     hpIcons2[i][j].fillAmount = playerhp * iconChild - 1 * j;
                    //     hpIcons3[i][j].fillAmount = playerhp * iconChild - 1 * j;
                    // }
                   
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
                        hpimage3Player4[i].enabled = true;
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

    private void SetHpIcons()
    {
        if(GameManager.Instance.joinPlayers == 2)
        {
            for(int j = 0; j < 10; j++)
            {
                hpIconsPlayerA[j] = hpIconParents[0].GetChild(j).GetComponent<Image>();
                hpIconsPlayerB[j] = hpIconParents[1].GetChild(j).GetComponent<Image>();
            }
        }
        else
        {
            for (int j = 0; j < 10; j++)
            {
                hpIconsPlayer1[j] = hpIconParents2[0].GetChild(j).GetComponent<Image>();
                hpIconsPlayer2[j] = hpIconParents2[1].GetChild(j).GetComponent<Image>();
                hpIconsPlayer3[j] = hpIconParents2[2].GetChild(j).GetComponent<Image>();
                hpIconsPlayer4[j] = hpIconParents2[3].GetChild(j).GetComponent<Image>();
            }
        }
    }

    private void PauseTitle()
    {
        pausePanel.SetActive(false);
        Pausetext.enabled = false;
        GameManager.Instance.GameEnd();
        SceneController.Instance.LoadLoadingScene(SCENE_NAME.GAME, SCENE_NAME.TITLE);
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
                Futont3imagePlayer4[i].enabled = true;
            }
            else
            {
                Futon1textPlayer4[i].enabled = false;
                FutontextPlayer4[i].enabled = false;
                FutontimagePlayer4[i].enabled = false;
                Futont3imagePlayer4[i].enabled = false;
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
                Futont3imagePlayer2[i].enabled = true;
            }
            else
            {
                Futon1textPlayer2[i].enabled = false;
                FutontextPlayer2[i].enabled = false;
                FutontimagePlayer2[i].enabled = false;
                Futont3imagePlayer2[i].enabled = false;
            }
        }
    }


    private void hpicon()
    {
        for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            //List<Image> images = new List<Image>();
            //for (int j = 0; j < iconChild; j++)
            //{
            //    images.Add(hpIconParents[i].GetChild(j).GetComponent<Image>());
            //}

            Image[] imgs = null;
            imgs = hpIconParents2[i].GetComponentsInChildren<Image>();
            List<Image> images = new List<Image>(imgs);
            hpIcons.Add(images);
        }
    }


    private void hpicon2()
    {
        for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            //List<Image> images = new List<Image>();
            //for (int j = 0; j < iconChild; j++)
            //{
            //    images.Add(hpIconParents2[i].GetChild(j).GetComponent<Image>());
            //}

            Image[] imgs = null;
            imgs = hpIconParents2[i].GetComponentsInChildren<Image>();

            List<Image> images = new List<Image>(imgs);
            hpIcons2.Add(images);
        }
        Debug.Log(hpIcons2.Count);
        Debug.Log(hpIcons2[0].Count);
    }

    /*
    private void hpicon3()
    {
        for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            List<Image> images = new List<Image>();
            for (int j = 0; j < iconChild; j++)
            {
                images.Add(hpIconParents3[i].GetChild(j).GetComponent<Image>());
            }
            hpIcons3.Add(images);
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
    private void hpicon4()
    {
        for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            List<Image> images = new List<Image>();
            for (int j = 0; j < iconChild; j++)
            {
                images.Add(hpIconParents4[i].GetChild(j).GetComponent<Image>());
            }
            hpIcons4.Add(images);
        }
    }
    private void hpicon5()
    {
        for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            List<Image> images = new List<Image>();
            for (int j = 0; j < iconChild; j++)
            {
                images.Add(hpIconParents5[i].GetChild(j).GetComponent<Image>());
            }
            hpIcons5.Add(images);
        }
    }
    */

    private const int maxHp = 10;
    private int[] hps = { maxHp, maxHp, maxHp, maxHp};
    private int joinPlayerNum = 0;

    private void ChangeHpEvent()
    {
        if(joinPlayerNum == 2)
        {
            for (int i = 0; i < joinPlayerNum; i++)
            {
                if(hps[i] != PlayerManager.Instance.playerDatas[i].HP)
                {
                    hps[i] = PlayerManager.Instance.playerDatas[i].HP;
                    if (damageUi2[i].activeSelf == true) damageUi2[i].SetActive(false);
                    damageUi2[i].SetActive(true);
                }
            }
        }
        else
        {
            for (int i = 0; i < joinPlayerNum; i++)
            {
                if (hps[i] != PlayerManager.Instance.playerDatas[i].HP)
                {
                    hps[i] = PlayerManager.Instance.playerDatas[i].HP;
                    if (damageUi4[i].activeSelf == true) damageUi4[i].SetActive(false);
                    damageUi4[i].SetActive(true);
                }
            }
        }
    }
}