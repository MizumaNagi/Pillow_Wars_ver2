using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{
    [SerializeField] private GameObject[] playerPrefabs;
    [SerializeField] private GameObject[] npcPrefabs;
    [SerializeField] private GameObject[] hairs;

    private Vector3[] spawnPos = { 
        new Vector3(-6f, 0, -6f),
        new Vector3(6f, 0, 6f),
        new Vector3(-6f, 0, 6f),
        new Vector3(6f, 0, -6f),
        new Vector3(3f, 0, -3f),
        new Vector3(-3f, 0, 3f),
        new Vector3(3f, 0, 3f),
        new Vector3(-3f, 0, -3f)
    };

    private Vector3[] spawnRot = { new Vector3(0, 45f, 0), new Vector3(0, 225f, 0), new Vector3(0, 135f, 0), new Vector3(0, 315f, 0), Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero };
    private Rect[] twoDivCameraRect = { new Rect(0, 0.5f, 1f, 1f), new Rect(0, 0, 1, 0.5f) };
    private Rect[] fourDivCameraRect = { new Rect(-0.5f, 0.5f, 1, 1), new Rect(0.5f, 0.5f, 1, 1), new Rect(-0.5f, -0.5f, 1, 1), new Rect(0.5f, -0.5f, 1, 1) };

    private Transform playersParent;
    private Transform npcsParent;

    public Transform PillowParent { get; private set; }
    public Transform CameraParent { get; private set; }
    public List<CharacterData> playerDatas = new List<CharacterData>();
    public List<CharacterData> npcDatas = new List<CharacterData>();

    public void Init()
    {
        GameObject emptyObj = new GameObject();
        playersParent = emptyObj.transform;
        emptyObj.name = "PlayersParent";

        GameObject emptyObj2 = new GameObject();
        npcsParent = emptyObj2.transform;
        emptyObj2.name = "NpcsParent";

        GameObject emptyObj3 = new GameObject();
        PillowParent = emptyObj3.transform;
        emptyObj3.name = "PillowParent";

        GameObject emptyObj4 = new GameObject();
        CameraParent = emptyObj4.transform;
        emptyObj4.name = "CameraParent";

        int charaIndex = 0;
        // player
        for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            int modelIndex = i % playerPrefabs.Length;
            GameObject chara = Instantiate(playerPrefabs[modelIndex], spawnPos[charaIndex], Quaternion.Euler(spawnRot[i]));
            chara.name = "Player" + i;
            chara.GetComponent<HitCharacterController>().objNum = i;
            chara.transform.SetParent(playersParent, true);
            EquipRandomHair(chara.transform);

            GameObject pillow = chara.transform.GetChild(0).GetChild(0).gameObject;
            pillow.name = i.ToString();
            pillow.transform.localPosition = InputManager.Instance.moveData.pillowSpawnPos;
            playerDatas.Add(new CharacterData(chara, i, false));

            pillow.GetComponent<PillowController>().characterData = playerDatas[i];

            charaIndex++;

            if (GameManager.Instance.joinPlayers == 2) playerDatas[i].myCamera.rect = twoDivCameraRect[i];
            else if (GameManager.Instance.joinPlayers == 4) playerDatas[i].myCamera.rect = fourDivCameraRect[i];
        }

        // npc
        for (int i = 0; i < GameManager.Instance.joinNpcs; i++)
        {
            int modelIndex = i % playerPrefabs.Length;
            GameObject chara = Instantiate(npcPrefabs[modelIndex], spawnPos[charaIndex], Quaternion.Euler(spawnRot[i]));
            chara.name = "Npc" + (i + 100);
            chara.GetComponent<HitCharacterController>().objNum = i + 100;
            chara.transform.SetParent(npcsParent, true);
            EquipRandomHair(chara.transform);

            GameObject pillow = chara.transform.GetChild(0).GetChild(0).gameObject;
            pillow.name = (i + 100).ToString();
            pillow.transform.localPosition = InputManager.Instance.moveData.pillowSpawnPos;
            npcDatas.Add(new CharacterData(chara, i + 100, true));

            pillow.GetComponent<PillowController>().characterData = npcDatas[i];

            charaIndex++;
        }

        InputManager.Instance.characterDatas = playerDatas;
    }

    public void DataReset()
    {
        playerDatas.Clear();
        npcDatas.Clear();
    }

    public void UpdateMethod()
    {
        CoolTimeElapse();
    }

    private void CoolTimeElapse()
    {
        for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            playerDatas[i].remainthrowCT -= Time.deltaTime;
            playerDatas[i].remainStunTime -= Time.deltaTime;
        }

        for (int i = 0; i < GameManager.Instance.joinNpcs; i++)
        {
            npcDatas[i].remainthrowCT -= Time.deltaTime;
            npcDatas[i].remainStunTime -= Time.deltaTime;
        }
    }

    private void EquipRandomHair(Transform charaObj)
    {
        int rndNum = Random.Range(0, hairs.Length + 1);

        // ƒnƒQ
        if (rndNum == hairs.Length) return;

        // ‚Ó‚³‚Ó‚³
        GameObject hair = Instantiate(hairs[rndNum]);
        hair.transform.parent = charaObj;
        hair.transform.localPosition = Vector3.zero;
        hair.transform.localRotation = Quaternion.identity;
    }
}