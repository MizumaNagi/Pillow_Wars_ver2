using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class NonHUDHpGaugeUpdate : MonoBehaviour
{
    /*
    private int myID;
    private int joinPlayers;
    private float[] npcHps;

    [SerializeField] private GameObject[] hpGauges;
    private List<Slider> sliders = new List<Slider>();

    private void Start()
    {
        joinPlayers = GameManager.Instance.joinPlayers;
        Array.Resize<float>(ref npcHps, joinPlayers);

        // myIDéÊìæ
        string name = transform.parent.gameObject.name;
        StringBuilder sb = new StringBuilder(name);
        sb.Replace("Npc", "");
        sb.Replace("Player", "");
        myID = int.Parse(sb.ToString());

        // êlêîï™ÇÃÇ›Hpï\é¶
        foreach(GameObject hpGauge in hpGauges)
        {
            hpGauge.SetActive(false);
        }
        for(int i = 0; i < joinPlayers; i++)
        {
            hpGauges[i].SetActive(true);
            sliders.Add(hpGauges[i].GetComponentInChildren<Slider>());
        }
    }

    private void Update()
    {
        if(!GameManager.Instance.isPause)
        {
            for(int i = 0; i < joinPlayers; i++)
            {
                hpGauges[i].transform.LookAt(PlayerManager.Instance.playerDatas[i].myCameraTransform);
                npcHps[i] = 1.0f * PlayerManager.Instance.npcDatas[myID - 100].HP / GameManager.Instance.ruleData.maxHp;
                sliders[i].value = npcHps[i];
            }
        }
    }
    */

    [SerializeField] private GameObject[] showNonHudHpUisEachPlayer;
    [SerializeField] private int playerId;

    private List<Slider> sliders = new List<Slider>();
    private int joinNpcs;
    private int joinPlayers;

    private void Start()
    {
        joinPlayers = GameManager.Instance.joinPlayers;
        joinNpcs = GameManager.Instance.joinNpcs;

        if (playerId >= joinPlayers) Destroy(this.gameObject);

        for(int i = 0; i < joinNpcs; i++)
        {
            sliders.Add(showNonHudHpUisEachPlayer[i].GetComponentInChildren<Slider>());
        }

        // Npcï™ÇÃÇ›ï\é¶
        for(int i = 3; i >= joinNpcs; --i)
        {
            showNonHudHpUisEachPlayer[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (PlayerManager.Instance.npcDatas.Count == 0) this.enabled = false;
        if (GameManager.Instance.isPause) return;

        for (int i = 0; i < joinNpcs; i++)
        {
            if (showNonHudHpUisEachPlayer[i].activeSelf == false) continue;

            showNonHudHpUisEachPlayer[i].transform.position = PlayerManager.Instance.npcDatas[i].myBodyTransform.localPosition + new Vector3(0,2,0);
            showNonHudHpUisEachPlayer[i].transform.LookAt(PlayerManager.Instance.playerDatas[playerId].myBodyTransform);

            int targetHp = PlayerManager.Instance.npcDatas[i].HP;
            if (targetHp <= 0)
            {
                showNonHudHpUisEachPlayer[i].SetActive(false);
                continue;
            }
            sliders[i].value = 1.0f * PlayerManager.Instance.npcDatas[i].HP / GameManager.Instance.ruleData.maxHp;
        }
    }

}
